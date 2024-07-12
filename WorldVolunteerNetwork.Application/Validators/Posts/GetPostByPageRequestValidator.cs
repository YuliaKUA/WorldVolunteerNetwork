using Contracts.Posts.Requests;
using FluentValidation;

namespace WorldVolunteerNetwork.Application.Validators.Posts
{
    public class GetPostByPageRequestValidator : AbstractValidator<GetPostsRequest>
    {
        public GetPostByPageRequestValidator()
        {
            RuleFor(x => x.Page).NotNull().GreaterThan(0);
            RuleFor(x => x.Size).NotNull().GreaterThan(0);
        }
    }
}
