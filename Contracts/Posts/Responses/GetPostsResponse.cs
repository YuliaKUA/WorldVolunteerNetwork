using Contracts.Posts.Dtos;

namespace Contracts.Posts.Responses
{
    public record GetPostsResponse(IEnumerable<PostDto> posts, int TotalCount);
}