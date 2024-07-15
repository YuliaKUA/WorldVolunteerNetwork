using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Posts.GetPosts;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Posts
{
    public class GetPostsQuery
    {
        private readonly WorldVolunteerNetworkReadDbContext _readDbContext;
        public GetPostsQuery(WorldVolunteerNetworkReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<GetPostsResponse> Handle(GetPostsRequest request, CancellationToken ct)
        {
            var postsQuery = _readDbContext.Posts
                .Where(p => string.IsNullOrWhiteSpace(request.Name) || p.Name.Contains(request.Name))
                .Where(p => string.IsNullOrWhiteSpace(request.Description) || p.Description.Contains(request.Description))
                .OrderBy(p => p.DateCreate);

            var totalCount = await postsQuery.CountAsync(cancellationToken: ct);

            var posts = await postsQuery
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync(cancellationToken: ct);

            return new GetPostsResponse(posts, totalCount);
        }
    }
}
