using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetPhoto;

namespace WorldVolunteerNetwork.Application.Features.Organizers.GetPhoto
{
    public class GetAllOrganizerPhotosQuery
    {
        private readonly IMinioProvider _minioProvider;
        private readonly WorldVolunteerNetworkReadDbContext _readDbContext;
   
        public GetAllOrganizerPhotosQuery(
            IMinioProvider minioProvider,
            WorldVolunteerNetworkReadDbContext readDbContext)
        {
            _minioProvider = minioProvider;
            _readDbContext = readDbContext;
        }
        public async Task<Result< GetAllOrganizerPhotoResponse, Error>> Handle(
            GetAllOrganizerPhotosRequest request,
            CancellationToken ct)
        {
            var photosPaths = _readDbContext.Organizers
                .Include(o => o.Photos)
                .Where(o => o.Id == request.OrganizerId)
                .SelectMany(o => o.Photos)
                .Select(p => p.Path)
                .ToListAsync(cancellationToken: ct);

            var photosUrls = await _minioProvider.GetPhotos(photosPaths.Result);
            if (photosUrls.IsFailure)
                return photosUrls.Error;

            return new GetAllOrganizerPhotoResponse(photosUrls.Value);
        }
    }
}
