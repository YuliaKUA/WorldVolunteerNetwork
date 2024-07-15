using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Abstractions;
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

        public async Task<Result<Guid, Error>> Add(Post post, CancellationToken ct)
        {
            await _writeDbContext.Posts.AddAsync(post, ct);


            var result = await _writeDbContext.SaveChangesAsync(ct);

            if (result == 0)
                return new Error("record.saving", "Post can not be save");

            return post.Id;
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
        public async Task<IReadOnlyList<Post>> GetByPage(int page, int size, CancellationToken ct)
        {
            return await _writeDbContext.Posts
                .AsNoTracking()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Post>> GetByFilter(string name, DateTimeOffset dateCreate)
        {
            var posts = await _writeDbContext.Posts.Where(p => p.Name.Contains(name)).ToListAsync();
            return posts;
        }
    }
}
