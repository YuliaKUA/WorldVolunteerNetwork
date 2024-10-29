using FluentValidation;

namespace WorldVolunteerNetwork.Application.Features.Posts.GetPosts
{
    public record GetPostsRequest(
        string? Name,
        string? Description,
        string? Status,
        int Page = 1,
        int Size = 10);

    public class GetPostsValidator : AbstractValidator<GetPostsRequest>
    {
        public GetPostsValidator()
        {
            RuleFor(x => x.Page).NotNull().GreaterThan(0);
            RuleFor(x => x.Size).NotNull().GreaterThan(0);
        }
    }
}
