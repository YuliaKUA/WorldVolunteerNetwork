using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Dtos;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetAllOrganizers
{
    public class GetAllOrganizersQuery
    {
        private readonly WorldVolunteerNetworkReadDbContext _readDbContext;

        public GetAllOrganizersQuery(WorldVolunteerNetworkReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }
        public async Task<GetOrganizersResponse> Handle()
        {
            var organizers = await _readDbContext.Organizers.ToListAsync();

            var organizersDtos = organizers.Select(o => new OrganizerDto(o.Id, o.FirstName, o.LastName, o.Patronymic, []));

            return new GetOrganizersResponse(organizersDtos);
        }
    }
}
