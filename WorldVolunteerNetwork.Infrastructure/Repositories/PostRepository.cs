using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Features.Posts;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Repositories
{
    public class PostRepository : IPostsRepository
    {
        private readonly WorldVolunteerNetworkWriteDbContext _writeDbContext;
        public PostRepository(WorldVolunteerNetworkWriteDbContext dbContext)
        {
            _writeDbContext = dbContext;
        }

        public async Task<Result<Post, Error>> GetById(Guid id)
        {
            var post = await _writeDbContext.Posts.FindAsync(id);

            if (post is null)
            {
                return Errors.General.NotFound(id);
            }

            return post;
        }
    }
}
