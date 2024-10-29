using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication
{
    public class ApplyVolunteerApplicationHandler
    {
        private readonly IWorldVolunteerNetworkWriteDbContext _dbContext;
        private readonly ILogger<ApplyVolunteerApplicationHandler> _logger;

        public ApplyVolunteerApplicationHandler(
            IWorldVolunteerNetworkWriteDbContext dbContext,
            ILogger<ApplyVolunteerApplicationHandler> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(ApplyVolunteerApplicationRequest request, CancellationToken ct)
        {
            var fullName = FullName.Create(
                request.FirstName, 
                request.LastName, 
                request.Patronymic).Value;

            var volunteerApplication = Domain.Entities.VolunteerApplication.Create(
               fullName,
               request.Email,
               request.YearsVolunteeringExperience,
               request.ExperienceDescription,
               request.IsMemberOfOrganization,
               request.NameOfOrganization).Value;

            await _dbContext.volunteerApplications.AddAsync(volunteerApplication, ct);
            await _dbContext.SaveChangesAsync(ct);

            _logger.LogInformation("Volunteer application has been created {id}", volunteerApplication.Id);

            return volunteerApplication.Id;

        }
    }
}
