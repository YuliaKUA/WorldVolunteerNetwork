using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Infrastructure.Queries.Posts;
using WorldVolunteerNetwork.Application.Features.Posts.GetPosts;

namespace WorldVolunteerNetwork.API.Controllers
{
    [Route("[controller]")]
    public class PostController : ApplicationController
    {
        

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
