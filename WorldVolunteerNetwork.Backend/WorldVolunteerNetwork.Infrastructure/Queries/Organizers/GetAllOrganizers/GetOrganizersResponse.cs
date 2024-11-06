using WorldVolunteerNetwork.Application.Dtos;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetAllOrganizers
{
    public record GetOrganizersResponse(IEnumerable<OrganizerDto> Organizers);
}
