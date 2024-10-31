using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Providers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorldVolunteerNetworkWriteDbContext _writeDbContext;

        public UnitOfWork(WorldVolunteerNetworkWriteDbContext writeDbContext) 
        {
            _writeDbContext = writeDbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
