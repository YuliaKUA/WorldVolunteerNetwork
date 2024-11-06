using FluentValidation;
using WorldVolunteerNetwork.Application.Validators;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication
{
    public record ApplyVolunteerApplicationRequest(
            string FirstName,
            string LastName,
            string Patronymic,

            string Email,

            int YearsVolunteeringExperience,

            string ExperienceDescription,
            bool IsMemberOfOrganization,

            string? NameOfOrganization
        );


    public class ApplyVolunteerApplicationRequestValidator : AbstractValidator<ApplyVolunteerApplicationRequest>
    {
        public ApplyVolunteerApplicationRequestValidator()
        {
            RuleFor(v => new { v.FirstName, v.LastName, v.Patronymic })
               .MustBeValueObject(v => FullName.Create(v.FirstName, v.LastName, v.Patronymic));

        }
    }
}

