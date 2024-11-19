using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;
using WorldVolunteerNetwork.Application.Features.Posts;
using WorldVolunteerNetwork.Application.Models;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreatePostWithPhoto
{
    public class CreatePostWithPhotoHandler
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IOrganizersRepository _organizersRepository;
        private readonly IUnitOfWork _writeDbContext;
        private readonly IMinioProvider _minioProvider;
        public CreatePostWithPhotoHandler(
            IPostsRepository postsRepository,
            IOrganizersRepository organizersRepository,
            IUnitOfWork dbContext,
            IMinioProvider minioProvider)
        {
            _postsRepository = postsRepository;
            _organizersRepository = organizersRepository;
            _writeDbContext = dbContext;
            _minioProvider = minioProvider;
        }
        public async Task<Result<Guid, Error>> Handle(
            CreatePostWithPhotoRequest request,
            CancellationToken ct)
        {
            // get organizer
            var organizer = await _organizersRepository.GetById(request.OrganizerId, ct);
            if (organizer.IsFailure)
            {
                return organizer.Error;
            }

            // create post
            var location = Location.Create(
                request.PostalCode,
                request.Country,
                request.City,
                request.Street,
                request.Building).Value;

            var contactNumber = PhoneNumber.Create(request.ContactNumber).Value;
            var status = PostStatus.Create(request.PostStatus).Value;
            var requirement = Requirement.Create(request.Age, request.Gender).Value;

            var photoFiles = GetPhotoStreams(request.Files);
            if (photoFiles.IsFailure)
                return photoFiles.Error;

            var photos = photoFiles.Value.Select(p => p.PostPhoto);

            var post = Post.Create(
                request.Name,
                request.Duration,
                request.Employment,
                request.Restriction,
                request.Description,
                request.Payment,
                request.Reward,
                location,
                contactNumber,
                status,
                requirement,
                request.SubmissionDeadline,
                request.DateCreate,
                photos);

            if (post.IsFailure)
                return post.Error;

            // add photo for post
            var isSuccessUpload = post.Value.AddPhoto(photos.ToList());
            if (isSuccessUpload.IsFailure)
            {
                return isSuccessUpload.Error;
            }

            // add post to organizer
            organizer.Value.PublishPost(post.Value);

            // save all changes
            await _writeDbContext.SaveChangesAsync(ct);


            // upload photos
            foreach (var photoFile in photoFiles.Value)
            {
                //open stream
                await using var stream = photoFile.File.OpenReadStream();
                // upload photo to minio
                var objectName = await _minioProvider.UploadPhoto(stream, photoFile.PostPhoto.Path);
                if (objectName.IsFailure)
                {
                    return objectName.Error;
                }
            }

            return organizer.Value.Id;
        }

        private Result<List<PhotoFile>, Error> GetPhotoStreams(
            IFormFileCollection fileCollection)
        {
            List<PhotoFile> photos = [];
            foreach (var file in fileCollection)
            {
                var contentType = Path.GetExtension(file.FileName);

                // create photo
                var photo = PostPhoto.Create(
                    contentType,
                    file.Length);

                if (photo.IsFailure)
                {
                    return photo.Error;
                }

                photos.Add(new PhotoFile(photo.Value, file));
            }

            return photos;
        }
    }
}