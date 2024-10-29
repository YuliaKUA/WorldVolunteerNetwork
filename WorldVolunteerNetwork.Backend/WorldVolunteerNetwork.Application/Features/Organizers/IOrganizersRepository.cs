using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Features.Organizers
{
    public interface IOrganizersRepository
    {
        Task Add(Organizer organizer, CancellationToken ct);
        Task<Result<Organizer, Error>> GetById(Guid id, CancellationToken ct);
        Task<Result<int, Error>> Save(CancellationToken ct);
    }
}
