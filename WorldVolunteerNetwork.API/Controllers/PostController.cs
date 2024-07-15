using Microsoft.AspNetCore.Mvc;
using Contracts.Requests;
using WorldVolunteerNetwork.Application;

namespace WorldVolunteerNetwork.API.Controllers
{
    [Route("[controller]")]
    public class PostController : ApplicationController
    {
        private readonly PostsService _postService;
        //private readonly IValidator<CreatePostRequest> _validator;

        public PostController(PostsService postsService)
        {
            _postService = postsService;
            //_validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken ct)
        {
            /// ! App uses auto-validation (see SharpFluentValidation library)

            //var result = await _validator.ValidateAsync(request, ct);
            //if (result.IsValid == false)
            //{
            //    return BadRequest(result.Errors);
            //}

            var idResult = await _postService.CreatePost(request, ct);

            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var posts = await _dbContext.Posts.ToListAsync();
            return Ok();

            //throw new Exception("Trouble!");
        }
    }
}
