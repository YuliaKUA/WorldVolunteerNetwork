using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Features.VolunteerApplication
{
    public interface IVolunteerApplicationRepository
    {
        Task Add(Domain.Entities.VolunteerApplication application, CancellationToken ct);
        Task<Result<Domain.Entities.VolunteerApplication, Error>> GetById(Guid id, CancellationToken ct);
    }
}
