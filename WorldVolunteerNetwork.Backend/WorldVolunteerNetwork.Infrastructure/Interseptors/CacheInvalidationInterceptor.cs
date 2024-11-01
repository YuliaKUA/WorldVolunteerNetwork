using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Providers;

namespace WorldVolunteerNetwork.Infrastructure.Interceptors
{
    public class CacheInvalidationInterceptor : SaveChangesInterceptor
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheInvalidationInterceptor(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default)
        {
            await InvalidateCache(eventData, cancellationToken);

            return result;
        }


        //public override async ValueTask<int> SavedChangesAsync(
        //    SaveChangesCompletedEventData eventData, 
        //    int result, 
        //    CancellationToken cancellationToken = default)
        //{
        //    await InvalidateCache(eventData, cancellationToken);

        //    return await base.SavedChangesAsync(eventData, result, cancellationToken);
        //}

        private async Task InvalidateCache(
            DbContextEventData eventData,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return;

            var entries = eventData.Context.ChangeTracker.Entries()
                    .Where(e => e.State
                        is EntityState.Added
                        or EntityState.Deleted
                        or EntityState.Modified);

            foreach (var entry in entries)
            {
                var entityName = entry.Entity.GetType().Name;
                await _cacheProvider.RemoveByPrefixAsync(entityName, cancellationToken);
            }
        }
    }
}
