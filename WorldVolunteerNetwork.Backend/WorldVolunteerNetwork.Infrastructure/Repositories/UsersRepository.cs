using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Application.Features.Accounts;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

namespace WorldVolunteerNetwork.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly WorldVolunteerNetworkWriteDbContext _writeDbContext;
        public UsersRepository(WorldVolunteerNetworkWriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task<Result<User, Error>> Add(User user, CancellationToken ct)
        {
            await _writeDbContext.AddAsync(user, ct);
            return user;
        }

        public async Task<Result<User, Error>> GetByEmail(string email, CancellationToken ct)
        {
            var user = await _writeDbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken: ct);

            if (user is null)
            {
                return Errors.General.NotFound(email);
            }

            return user;
        }
    }
}
