using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Dtos;
using WorldVolunteerNetwork.Application.Providers;
using WorldVolunteerNetwork.Infrastructure.DbContexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetAllOrganizers
{
    public class GetAllOrganizersQuery
    {
        private readonly WorldVolunteerNetworkReadDbContext _readDbContext;
        private readonly ICacheProvider _cacheProvider;

        public GetAllOrganizersQuery(
            WorldVolunteerNetworkReadDbContext readDbContext,
            ICacheProvider cache)
        {
            _readDbContext = readDbContext;
            _cacheProvider = cache;
        }
        public async Task<GetOrganizersResponse> Handle(CancellationToken ct)
        {
            return await _cacheProvider.GetOrSetAsync(
                CacheKeys.Organizers, 
                async () => 
                {
                    var organizers = await _readDbContext.Organizers.ToListAsync(ct);

                    var organizersDtos = organizers.Select(o =>
                        new OrganizerDto(o.Id, o.FirstName, o.LastName, o.Patronymic, []));
                    
                    return new GetOrganizersResponse(organizersDtos);

                },
                ct) ?? new([]);


        }

        public async Task<GetOrganizersResponse> Handle()
        {
            var organizers = await _readDbContext.Organizers.ToListAsync();

            var organizersDtos = organizers.Select(o => new OrganizerDto(o.Id, o.FirstName, o.LastName, o.Patronymic, []));

            return new GetOrganizersResponse(organizersDtos);
        }
    }

}
