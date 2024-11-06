using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication
{
    public class ApplyVolunteerApplicationHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVolunteerApplicationRepository _volunteerApplicationRepository;
        private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

        public ApplyVolunteerApplicationHandler(
            IUnitOfWork dbContext,
            IVolunteerApplicationRepository volunteerApplicationRepository,
            ILogger<ApplyVolunteerApplicationHandler> logger) 
        {
            _unitOfWork = dbContext;
            _volunteerApplicationRepository = volunteerApplicationRepository;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(ApplyVolunteerApplicationRequest request, CancellationToken ct)
        {
            var fullName = FullName.Create(
                request.FirstName, 
                request.LastName, 
                request.Patronymic).Value;

            var email = Email.Create(request.Email).Value;

            var volunteerApplication = Domain.Entities.VolunteerApplication.Create(
               fullName,
               email,
               request.YearsVolunteeringExperience,
               request.ExperienceDescription,
               request.IsMemberOfOrganization,
               request.NameOfOrganization).Value;

            await _volunteerApplicationRepository.Add(volunteerApplication, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Volunteer application has been created {id}", volunteerApplication.Id);

            return volunteerApplication.Id;

        }
    }
}
