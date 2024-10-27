using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Features.Accounts.Login
{
    public class LoginHandler
    {
        private readonly IWorldVolunteerNetworkWriteDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(
            IWorldVolunteerNetworkWriteDbContext dbContext,
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            ILogger<LoginHandler> logger)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }
        public async Task<Result<string, Error>> Handle(LoginRequest request, CancellationToken ct)
        {
            //check user exist
            var user = await _userRepository.GetByEmail(request.Email, ct);

            if (user.IsFailure)
            {
                return user.Error;
            }

            //check password (hash)
            var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Value.PasswordHash);

            if (isVerified == false)
            {
                return Errors.Users.InvalidData();
            }

            //generate token
            var token = _jwtProvider.Generate(user.Value);


            //return token
            return token;
        }

    }
}
