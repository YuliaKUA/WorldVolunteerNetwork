using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Repositories
{
    public class VolunteerApplicationRepository : IVolunteerApplicationRepository
    {
        private readonly IWorldVolunteerNetworkWriteDbContext _writeDbContext;
        public VolunteerApplicationRepository(IWorldVolunteerNetworkWriteDbContext writeDbContext) 
        {
            _writeDbContext = writeDbContext;
        }

        public async Task Add(VolunteerApplication application, CancellationToken ct)
        {
            await _writeDbContext.volunteerApplications.AddAsync(application, ct);
        }

        public async Task<Result<VolunteerApplication, Error>> GetById(Guid id, CancellationToken ct)
        {
            var application = await _writeDbContext.volunteerApplications
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

            if (application is null) 
            {
                return Errors.General.NotFound(id);
            }

            return application;
        }
    }
}
