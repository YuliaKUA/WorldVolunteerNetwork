using FluentValidation;
using WorldVolunteerNetwork.Application.Validators;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Posts.CreatePost;

//DTO (Data Transfer Object )
public record CreatePostRequest(
    Guid OrganizerId,

    string Name,

    string PostalCode,
    string Country,
    string City,
    string Street,
    string Building,

    string Duration,
    string Employment,
    string Restriction,
    string Description,

    string ContactNumber,

    string PostStatus,

    string Age,
    string Gender,

    float Reward,
    float Payment,

    DateTimeOffset SubmissionDeadline,
    DateTimeOffset DateCreate
);

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
