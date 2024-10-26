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
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(
            IWorldVolunteerNetworkWriteDbContext dbContext,
            IUserRepository userRepository,
            ILogger<LoginHandler> logger)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
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
            var jwtHandler = new JsonWebTokenHandler();
            var key = "secretKeyFromConfigurationKeyKey";

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var permissionClaims = user.Value.Role.Permissions
                .Select(p => new Claim(Constants.Constants.Permissions, p));

            var claims = permissionClaims.Concat(
                [
                    new Claim(Constants.Constants.Role, user.Value.Role.RoleName),
                    new Claim(Constants.Constants.UserId, user.Value.Id.ToString()),
                ]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(claims),
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256),
                //Expires = DateTime.,
            };


            var token = jwtHandler.CreateToken(tokenDescriptor);


            //return token
            return token;
        }

    }
}
