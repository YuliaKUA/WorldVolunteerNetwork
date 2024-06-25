using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Domain.Entities;
using static WorldVolunteerNetwork.API.Contracts.PostController;

namespace WorldVolunteerNetwork.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class PostController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken ct)
        {
            var post = new Post(
                request.Name,
                request.Location,
                request.Payment,
                request.Reward,
                request.Duration,
                request.Employment,
                request.Restriction,
                request.SubmissionDeadline,
                request.Description,
                request.Photo
                );

            return Ok();
        }
    }
}
