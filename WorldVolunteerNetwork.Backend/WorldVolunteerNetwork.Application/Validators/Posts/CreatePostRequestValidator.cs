using FluentValidation;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Validators.Posts
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => new { x.PostalCode, x.Country, x.City, x.Street, x.Building })
                .MustBeValueObject(x => Location.Create(x.PostalCode, x.Country, x.City, x.Street, x.Building));
            RuleFor(x => x.ContactNumber).MustBeValueObject(PhoneNumber.Create);
            RuleFor(x => x.PostStatus).MustBeValueObject(PostStatus.Create);
            RuleFor(x => new { x.Age, x.Gender }).MustBeValueObject(x => Requirement.Create(x.Age, x.Gender));
        }
    }
}
