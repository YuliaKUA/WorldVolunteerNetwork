using FluentValidation;
using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Application.Validators;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer
{
    public record CreateOrganizerRequest(
        Guid AccountId,

        string Name,
        string? Description,

        int VolunteeringExperience,

        bool ActsBehalfCharitableOrganization,

        List<CreateSocialMediaRequest> SocialMedias);

    public class CreateOrganizerRequestValidator : AbstractValidator<CreateOrganizerRequest>
    {
        public CreateOrganizerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmptyWithError()
                .MaximumLengthWithError(Constraints.SHORT_TITLE_LENGTH);
            RuleFor(x => x.Description)
                .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);
        }
    }
}
