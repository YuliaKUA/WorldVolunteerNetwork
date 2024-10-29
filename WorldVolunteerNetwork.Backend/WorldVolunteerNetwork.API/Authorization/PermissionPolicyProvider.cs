using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WorldVolunteerNetwork.API.Attributes;

namespace WorldVolunteerNetwork.API.Authorization
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        ///create politics with all our permissions
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new HasPermissionsAttribute(policyName))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);

        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            Task.FromResult<AuthorizationPolicy?>(null);

    }
}
