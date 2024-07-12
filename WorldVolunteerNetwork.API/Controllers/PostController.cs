using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Application.Services;
using Contracts.Posts.Requests;
using WorldVolunteerNetwork.Infrastructure.Queries;

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
        public async Task<IActionResult> Get(
            [FromServices] GetPostsQuery query,
            [FromQuery] GetPostsRequest request,
            CancellationToken ct)
        {
            var response = await query.Handle(request, ct);
            return Ok(response);
        }
    }
}
