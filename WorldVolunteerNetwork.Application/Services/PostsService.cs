using Contracts.Posts.Requests;
using Contracts.Posts.Responses;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Mapping;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;


namespace WorldVolunteerNetwork.Application.Services
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
                request.Building).Value;

            var contactNumber = PhoneNumber.Create(request.ContactNumber).Value;
            var status = PostStatus.Create(request.PostStatus).Value;
            var requirement = Requirement.Create(request.Age, request.Gender).Value;

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
                request.DateCreate);

            var idResult = await _postsRepository.Add(post.Value, ct);
            if (idResult.IsFailure)
                return idResult.Error;

            return idResult;
        }

        public async Task<GetPostsResponse> Get(GetPostsRequest request, CancellationToken ct)
        {
            var posts = await _postsRepository.GetByPage(request.Page, request.Size, ct);

            var postDtos = posts.Select(p => p.ToDto());

            return new GetPostsResponse(postDtos, 1);
        }
    }
}
