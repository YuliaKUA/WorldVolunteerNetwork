using Microsoft.AspNetCore.Mvc;
using Contracts.Requests;
using WorldVolunteerNetwork.Application;

namespace WorldVolunteerNetwork.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostsService _postService;

        public PostController(PostsService postsService)
        {
            _postService = postsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken ct)
        {
            var idResult = await _postService.CreatePost(request, ct);
            if (idResult.IsFailure)
                return BadRequest(idResult.Error);

            return Ok(idResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var posts = await _dbContext.Posts.ToListAsync();
            return Ok();
        }
    }
}
