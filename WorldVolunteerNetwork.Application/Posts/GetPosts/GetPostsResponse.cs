using WorldVolunteerNetwork.Application.Dtos;

namespace WorldVolunteerNetwork.Application.Posts.GetPosts
{
    public record GetPostsResponse(IEnumerable<PostDto> posts, int TotalCount);
}