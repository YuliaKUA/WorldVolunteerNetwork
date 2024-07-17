using Dapper;
using WorldVolunteerNetwork.Application.Dtos;
using WorldVolunteerNetwork.Application.Features.Posts.GetPosts;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Posts
{
    // See SQRS pattern
    // These methods are only used in the controller (with other models - response)
    // isolated from repositories
    public class GetAllPostsQuery
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        public GetAllPostsQuery(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<GetPostsResponse> Handle()
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
                SELECT 
                p.id,
                p.name,
                p.duration,
                p.description,
                p.status,
                p.reward,
                p.submission_deadline,
                p.date_create,
                ph.id,
                ph.path,
                ph.is_main
                FROM posts p
                LEFT JOIN photos ph ON p.id = ph.post_id
                """;

            Dictionary<Guid, PostDto> postsDictionary = new();

            await connection.QueryAsync<PostDto, PhotoDto, PostDto>(
                sql, 
                (post, photo) =>
                {
                    if(postsDictionary.TryGetValue(post.Id, out var existingPost))
                    {
                        post = existingPost;
                    }
                    else
                    {
                        postsDictionary.Add(post.Id, post);
                    }

                    post.Photos.Add(photo);
                    return post;
                },
                splitOn: "id");

            return new GetPostsResponse(postsDictionary.Select(p => p.Value), 10); 
        }
    }
}
