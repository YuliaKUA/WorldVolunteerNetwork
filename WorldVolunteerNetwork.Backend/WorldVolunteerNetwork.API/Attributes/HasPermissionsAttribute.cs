using Microsoft.AspNetCore.Authorization;

namespace WorldVolunteerNetwork.API.Attributes
{
    public class HasPermissionsAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Permission { get; }
        public HasPermissionsAttribute(string permission) : base(permission)
        {
            Permission = permission;
        }

    }
}
