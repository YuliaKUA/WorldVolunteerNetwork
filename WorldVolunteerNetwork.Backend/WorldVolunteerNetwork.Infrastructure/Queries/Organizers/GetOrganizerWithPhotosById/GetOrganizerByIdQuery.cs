using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Dtos;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetOrganizer;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetOrganizer
{
    public class GetOrganizerByIdQuery
    {
        private readonly IMinioProvider _minioProvider;
        private readonly WorldVolunteerNetworkReadDbContext _readDbContext;

        public GetOrganizerByIdQuery(
            IMinioProvider minioProvider,
            WorldVolunteerNetworkReadDbContext readDbContext)
        {
            _minioProvider = minioProvider;
            _readDbContext = readDbContext;
        }
        public async Task<Result<GetOrganizerByIdResponse, Error>> Handle(
            GetAllOrganizerRequest request,
            CancellationToken ct)
        {
            var organizer = await _readDbContext.Organizers
                .Include(o => o.Photos)
                .FirstOrDefaultAsync(o => o.Id == request.OrganizerId, cancellationToken: ct);

            if (organizer is null)
            {
                return Errors.General.NotFound(request.OrganizerId);
            }

            var photosPaths = organizer.Photos.Select(p => p.Path);

            var photosUrls = await _minioProvider.GetPhotos(photosPaths);
            if (photosUrls.IsFailure)
                return photosUrls.Error;

            var organizerDto = new OrganizerDto(
                organizer.Id,
                organizer.FirstName,
                organizer.LastName,
                organizer.Patronymic,
                organizer.Photos.Select(p => new OrganizerPhotoDto
                {
                    Id = p.Id,
                    Path = p.Path,
                    OrganizerId = organizer.Id,
                    IsMain = p.IsMain
                }).ToList()
            );

            return new GetOrganizerByIdResponse(organizerDto);


        }
    }
}
