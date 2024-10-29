using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Features.Accounts
{
    public interface IUserRepository
    {
        Task<Result<User, Error>> GetByEmail(string email, CancellationToken ct);
    }
}
