using WorldVolunteerNetwork.Application.Dtos;

namespace WorldVolunteerNetwork.Application.Features.Posts.GetPosts
{
    public record GetPostsResponse(IEnumerable<PostDto> posts, int TotalCount);
}