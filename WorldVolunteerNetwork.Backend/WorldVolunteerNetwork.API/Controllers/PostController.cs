using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Infrastructure.Queries.Posts;
using WorldVolunteerNetwork.Application.Features.Posts.GetPosts;
using WorldVolunteerNetwork.Application.Dtos;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Minio.DataModel.ILM;
using System.Collections.Generic;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.API.Controllers
{
    public class PostController : ApplicationController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PostDto> postDtos =
                [
                    new(
                        Guid.NewGuid(),
                        "Saving the Green Turtles",
                        "2 month",
                        "it is necessary to come to the island and work under the guidance of experienced instructors.",
                        PostStatus.Active.Value,
                        1000,
                        DateTimeOffset.Now,
                        DateTimeOffset.Now,
                        []
                    ),
                    new(
                        Guid.NewGuid(),
                        "Saving the Green Turtles",
                        "2 month",
                        "it is necessary to come to the island and work under the guidance of experienced instructors.",
                        PostStatus.Active.Value,
                        1000,
                        DateTimeOffset.Now,
                        DateTimeOffset.Now,
                        []
                    ),
                    new(
                        Guid.NewGuid(),
                        "Saving the Green Turtles",
                        "2 month",
                        "it is necessary to come to the island and work under the guidance of experienced instructors.",
                        PostStatus.Active.Value,
                        1000,
                        DateTimeOffset.Now,
                        DateTimeOffset.Now,
                        []
                    ),
                    new(
                        Guid.NewGuid(),
                        "Saving the Green Turtles",
                        "2 month",
                        "it is necessary to come to the island and work under the guidance of experienced instructors.",
                        PostStatus.Active.Value,
                        1000,
                        DateTimeOffset.Now,
                        DateTimeOffset.Now,
                        []
                    ),
                ];

            return Ok(postDtos);
        }

        //[HttpGet("ef-core")]
        //public async Task<IActionResult> Get(
        //    [FromServices] GetPostsQuery query,
        //    [FromQuery] GetPostsRequest request,
        //    CancellationToken ct)
        //{
        //    var response = await query.Handle(request, ct);
        //    return Ok(response);
        //}

        //[HttpGet("dapper")]
        //public async Task<IActionResult> Get(
        //    [FromServices] GetAllPostsQuery query,
        //    [FromQuery] GetPostsRequest request,
        //    CancellationToken ct)
        //{
        //    var response = await query.Handle();
        //    return Ok(response);
        //}
    }
}
