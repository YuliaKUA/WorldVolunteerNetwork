using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Repositories
{
    public class PostRepository : IPostsRepository
    {
        private readonly WorldVolunteerNetworkDbContext _dbContext;
        public PostRepository(WorldVolunteerNetworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid, Error>> Add(Post post, CancellationToken ct)
        {
            await _dbContext.Posts.AddAsync(post, ct);


            var result = await _dbContext.SaveChangesAsync(ct);

            if (result == 0)
                return new Error("record.saving", "Post can not be save");

            return post.Id;
        }
    }
}
