﻿using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto
{
    public class UploadOrganizerPhotoHandler
    {
        private readonly IMinioProvider _minioProvider;
        private readonly IOrganizersRepository _organizersRepository;
        public UploadOrganizerPhotoHandler(
            IMinioProvider minioProvider,
            IOrganizersRepository organizersRepository)
        {
            _minioProvider = minioProvider;
            _organizersRepository = organizersRepository;
        }
        public async Task<Result<string, Error>> Handle(
            UploadOrganizerPhotoRequest request,
            CancellationToken ct)
        {
            // get organizer by id
            var organizer = await _organizersRepository.GetById(request.OrganizerId, ct);
            if (organizer.IsFailure)
            {
                return organizer.Error;
            }

            var photoId = Guid.NewGuid();
            var path = photoId + Path.GetExtension(request.File.FileName);

            // create photo
            var photo = OrganizerPhoto.Create(
                path,
                request.isMain);

            if (photo.IsFailure)
            {
                return photo.Error;
            }

            // upload photo for organizer
            var isSuccessUpload = organizer.Value.AddPhoto(photo.Value);
            if (isSuccessUpload.IsFailure)
            {
                return isSuccessUpload.Error;
            }

            // upload photo to minio
            var objectName = await _minioProvider.UploadPhoto(request.File, path);
            if (objectName.IsFailure)
            {
                return objectName.Error;
            }

            // save photo to DB (for organizer)
            var result = await _organizersRepository.Save(ct);
            if (result.IsFailure)
            {
                await _minioProvider.RemovePhoto(path);
                return result.Error;
            }

            return path;
        }
    }
}
