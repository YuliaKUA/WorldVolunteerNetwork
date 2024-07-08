using Contracts.Requests;
using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;


namespace WorldVolunteerNetwork.Application
{
    public class PostsService
    {
        private readonly IPostsRepository _postsRepository;
        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }
        public async Task<Result<Guid, Error>> CreatePost(CreatePostRequest request, CancellationToken ct)
        {
            var location = Location.Create(
                request.PostalCode,
                request.Country,
                request.City,
                request.Street,
                request.Building);
            if (location.IsFailure)
                return location.Error;

            var contactNumber = PhoneNumber.Create(request.ContactNumber);
            if (contactNumber.IsFailure)
                return contactNumber.Error;

            var status = PostStatus.Create(request.PostStatus);
            if (status.IsFailure)
                return status.Error;

            var requirement = Requirement.Create(request.Age, request.Gender);
            if (requirement.IsFailure)
                return requirement.Error;

            var post = Post.Create(
                request.Name,
                request.Duration,
                request.Employment,
                request.Restriction,
                request.Description,
                request.Payment,
                request.Reward,
                location.Value,
                contactNumber.Value,
                status.Value,
                requirement.Value,
                request.SubmissionDeadline,
                request.DateCreate);

            var idResult = await _postsRepository.Add(post.Value, ct);
            if (idResult.IsFailure)
                return idResult.Error;

            return post.Value.Id;
        }
    }
}
