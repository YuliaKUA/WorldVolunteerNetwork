using Microsoft.AspNetCore.Authorization;
using WorldVolunteerNetwork.API.Attributes;
using WorldVolunteerNetwork.Application.Constants;

namespace WorldVolunteerNetwork.API.Authorization
{
    public class PermissionsAuthorizationsHandler : AuthorizationHandler<HasPermissionsAttribute>
    {
        private readonly ILogger<PermissionsAuthorizationsHandler> _logger;

        public PermissionsAuthorizationsHandler(ILogger<PermissionsAuthorizationsHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionsAttribute requirement)
        {
            var permission = context.User.Claims
                .Where(c => c.Type == Constants.Authentication.Permissions)
                .Select(c => c.Value);

            if (!permission.Contains(requirement.Permission))
            {
                _logger.LogError("User has not permission : {permission}, is successfull", requirement.Permission);
                return Task.CompletedTask;
            }

            _logger.LogInformation("User has permission : {permission}, is successfull", requirement.Permission);
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
