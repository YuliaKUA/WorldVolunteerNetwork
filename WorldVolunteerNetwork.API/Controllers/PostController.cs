using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure;
using static WorldVolunteerNetwork.API.Contracts.PostController;

namespace WorldVolunteerNetwork.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class PostController : ControllerBase
    {
        private readonly WorldVolunteerNetworkDbContext _dbContext;

        public PostController(WorldVolunteerNetworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return Ok(posts);
        }
    }
}
