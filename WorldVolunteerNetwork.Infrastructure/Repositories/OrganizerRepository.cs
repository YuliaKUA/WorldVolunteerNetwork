using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Features.Organizers;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Repositories
{
    public class OrganizerRepository : IOrganizersRepository
    {
        private readonly WorldVolunteerNetworkWriteDbContext _writeDbContext;
        public OrganizerRepository(WorldVolunteerNetworkWriteDbContext dbContext)
        {
            _writeDbContext = dbContext;
        }

        public async Task Add(Organizer organizer, CancellationToken ct)
        {
            await _writeDbContext.Organizers.AddAsync(organizer, ct);
        }

        public async Task<Result<Organizer, Error>> GetById(Guid id, CancellationToken ct)
        {
            var organizer = await _writeDbContext.Organizers
                .Include(o => o.Posts)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken: ct);

            if (organizer is null)
            {
                return Errors.General.NotFound(id);
            }

            return organizer;
        }

        public async Task<Result<int, Error>> Save(CancellationToken ct)
        {
            //_writeDbContext.Organizers.Attach(organizer);
            //var state = _writeDbContext.Entry(organizer).State;

            var result = await _writeDbContext.SaveChangesAsync(ct);

            if (result == 0)
                return Errors.General.SaveFailure("Organizer");

            return result;
        }

        public async Task<Result<int, Error>> Attach(CancellationToken ct)
        {
            //_writeDbContext.Organizers.Attach(organizer);
            //var state = _writeDbContext.Entry(organizer).State;

            var result = await _writeDbContext.SaveChangesAsync(ct);

            if (result == 0)
                return Errors.General.SaveFailure("Organizer");

            return result;
        }
    }
}
