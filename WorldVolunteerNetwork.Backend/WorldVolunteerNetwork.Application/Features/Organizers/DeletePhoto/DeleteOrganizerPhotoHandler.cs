using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Features.Organizers.DeletePhoto
{
    public class DeleteOrganizerPhotoHandler
    {
        private readonly IMinioProvider _minioProvider;
        private readonly IOrganizersRepository _organizersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrganizerPhotoHandler( 
            IMinioProvider minioProvider,
            IOrganizersRepository organizersRepository,
            IUnitOfWork unitOfWork)
        {
            _minioProvider = minioProvider;
            _organizersRepository = organizersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool, Error>> Handle(
            DeleteOrganizerPhotoRequest request,
            CancellationToken ct)
        {
            var organizer = await _organizersRepository.GetById(request.OrganizerId, ct);
            if (organizer.IsFailure) {
                return organizer.Error;
            }

            var isRemove = await _minioProvider.RemovePhoto(request.Path);
            if (isRemove.IsFailure) { 
                return isRemove.Error;
            }

            var isDelete = organizer.Value.DeletePhoto(request.Path);
            if (isDelete.IsFailure) { 
                return isDelete.Error;
            }

            var result = await _unitOfWork.SaveChangesAsync(ct);

            return true;
        }
    }
}
