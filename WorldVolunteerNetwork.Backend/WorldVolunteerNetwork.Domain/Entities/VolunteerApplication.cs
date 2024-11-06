using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class VolunteerApplication : Common.Entity
    {
        private VolunteerApplication() { }
        private VolunteerApplication(
            FullName fullName,
            Email email,
            int yearsVolunteeringExperience,
            string experienceDescription,
            bool isMemberOfOrganization,
            string? nameOfOrganization) 
        {
            FullName = fullName;
            Email = email;
            YearsVolunteeringExperience = yearsVolunteeringExperience;
            ExperienceDescription = experienceDescription;
            IsMemberOfOrganization = isMemberOfOrganization;
            NameOfOrganization = nameOfOrganization;
            StatusApplication = StatusApplication.Reviewed;
        }

        public FullName FullName { get; private set; }
        public int YearsVolunteeringExperience { get; private set; }

        public string? ExperienceDescription { get; private set; }
        public bool IsMemberOfOrganization { get; private set; }
        public string? NameOfOrganization { get; private set; }
        public StatusApplication StatusApplication { get; private set; }
        public Email Email {  get; private set; }

        public static Result<VolunteerApplication, Error> Create(
            FullName fullName,
            Email email,
            int yearsVolunteeringExperience,
            string experienceDescription,
            bool isMemberOfOrganization,
            string nameOfOrganization
            )
        {
            if (experienceDescription.IsEmpty())
            {
                return Errors.General.ValueIsRequired("volunteerApplication: experience description");
            }

            return new VolunteerApplication(
                fullName,
                email,
                yearsVolunteeringExperience,
                experienceDescription,
                isMemberOfOrganization,
                nameOfOrganization);
        }

        public Result<bool, Error> Approve()
        {
            if (StatusApplication == StatusApplication.Approved)
                return Errors.VolunteersApplications.AlredyApproved();

            StatusApplication = StatusApplication.Approved;
            return true;
        }
        public void Deny()
        {
            StatusApplication = StatusApplication.Denied;
        }
    }
}
