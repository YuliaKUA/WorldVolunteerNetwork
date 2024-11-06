﻿using Contracts.Requests;
using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Posts;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;


<<<<<<<< HEAD:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Application/Features/Organizers/CreatePost/CreatePostsHandler.cs
namespace WorldVolunteerNetwork.Application.Features.Organizers.CreatePost
{
    public class CreatePostsHandler
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IOrganizersRepository _organizersRepository;
        private readonly IUnitOfWork _writeDbContext;
        public CreatePostsHandler(
            IPostsRepository postsRepository, 
            IOrganizersRepository organizersRepository, 
            IUnitOfWork dbContext)
========
namespace WorldVolunteerNetwork.Application
{
    public class PostsService
    {
        private readonly IPostsRepository _postsRepository;
        public PostsService(IPostsRepository postsRepository)
>>>>>>>> origin/main:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Application/PostsService.cs
        {
            _postsRepository = postsRepository;
            _organizersRepository = organizersRepository;
            _writeDbContext = dbContext;
        }
        public async Task<Result<Guid, Error>> CreatePost(CreatePostRequest request, CancellationToken ct)
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

            if (post.IsFailure) 
                return post.Error;

            // add post to organizer
            organizer.Value.PublishPost(post.Value);

            //var idResult = await _organizersRepository.Save(ct);

            //if (idResult.IsFailure)
            //    return idResult.Error;

            await _writeDbContext.SaveChangesAsync(ct);


            return organizer.Value.Id;
        }
    }
}