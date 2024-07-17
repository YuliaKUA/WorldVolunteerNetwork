using FluentValidation;
using Microsoft.AspNetCore.Http;

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

        }
    }
}
