using FluentValidation;
using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Application.Validators;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer
{
    public record CreateOrganizerRequest(
        Guid AccountId,

        string FirstName,
        string LastName,
        string? Patronymic,
        string? Description,

        int VolunteeringExperience,

        bool ActsBehalfCharitableOrganization,

        List<CreateSocialMediaRequest> SocialMedias);

    public class CreateOrganizerRequestValidator : AbstractValidator<CreateOrganizerRequest>
    {
        public CreateOrganizerRequestValidator()
        {
            RuleFor(v => new { v.FirstName, v.LastName, v.Patronymic })
                .MustBeValueObject(v => FullName.Create(v.FirstName, v.LastName, v.Patronymic));

            RuleFor(x => x.Description)
                .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

            //RuleFor(x => x.VolunteeringExperience).GreaterThanWithError(0);

            RuleForEach(x => x.SocialMedias).ChildRules(s =>
            {
                s.RuleFor(x => x.Link)
                    .NotEmptyWithError()
                    .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
                s.RuleFor(x => x.Social).MustBeValueObject(Social.Create);
            }).When(x => x.SocialMedias != null);
        }
    }
}
