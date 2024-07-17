using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;

namespace WorldVolunteerNetwork.API.Controllers
{
    [Route("[controller]")]
    public class OrganizerController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] CreateOrganizersService service,
            [FromBody] CreateOrganizerRequest request,
            CancellationToken ct)
        {
            var idResult = await service.Handle(request, ct);
            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }

        [HttpPost("post")]
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
    }
}
