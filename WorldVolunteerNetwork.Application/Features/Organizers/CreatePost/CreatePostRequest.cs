using FluentValidation;
using WorldVolunteerNetwork.Application.Validators;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;

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

        RuleFor(x => x.Name)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);

        RuleFor(x => x.PostalCode).NotEmptyWithError();
        RuleFor(x => x.Country).NotEmptyWithError();
        RuleFor(x => x.City).NotEmptyWithError();
        RuleFor(x => x.Street).NotEmptyWithError();
        RuleFor(x => x.Building).NotEmptyWithError();

        RuleFor(x => x.Duration)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        RuleFor(x => x.Employment)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
        RuleFor(x => x.Restriction).MaximumLengthWithError(Constraints.MEDIUM_TITLE_LENGTH);
        RuleFor(x => x.Description)
            .NotEmptyWithError()
            .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
    }
}
