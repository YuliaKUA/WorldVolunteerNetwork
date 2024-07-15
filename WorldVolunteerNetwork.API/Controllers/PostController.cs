using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Application.Posts.CreatePost;
using WorldVolunteerNetwork.Infrastructure.Queries.Posts;
using WorldVolunteerNetwork.Application.Posts.GetPosts;

namespace WorldVolunteerNetwork.API.Controllers
{
    [Route("[controller]")]
    public class PostController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] CreatePostsService postsService,
            [FromBody] CreatePostRequest request, 
            CancellationToken ct)
        {
            /// ! App uses auto-validation (see SharpFluentValidation library)

            //var result = await _validator.ValidateAsync(request, ct);
            //if (result.IsValid == false)
            //{
            //    return BadRequest(result.Errors);
            //}

            var idResult = await postsService.Handle(request, ct);

            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }

        [HttpGet("ef-core")]
        public async Task<IActionResult> Get(
            [FromServices] GetPostsQuery query,
            [FromQuery] GetPostsRequest request,
            CancellationToken ct)
        {
            var response = await query.Handle(request, ct);
            return Ok(response);
        }

        [HttpGet("dapper")]
        public async Task<IActionResult> Get(
            [FromServices] GetAllPostsQuery query,
            [FromQuery] GetPostsRequest request,
            CancellationToken ct)
        {
            var response = await query.Handle();
            return Ok(response);
        }
    }
}
