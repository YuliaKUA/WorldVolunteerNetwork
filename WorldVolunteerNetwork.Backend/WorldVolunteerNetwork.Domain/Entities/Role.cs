
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Role : Entity
    {
        public static readonly Role Admin = new Role(
            Guid.NewGuid(),
            nameof(Admin).ToUpper(), 
            [
                Common.Permissions.VolunteerApplications.Read,
                Common.Permissions.VolunteerApplications.Update,

                Common.Permissions.Organizers.Create,
                Common.Permissions.Organizers.Read,
                Common.Permissions.Organizers.Delete,

                Common.Permissions.Posts.Read,
                Common.Permissions.Posts.Delete
            ]);

        public static readonly Role Organizer = new Role(
            Guid.NewGuid(),
            nameof(Organizer).ToUpper(), 
            [
                Common.Permissions.Posts.Create,
                Common.Permissions.Posts.Read,
                Common.Permissions.Posts.Update,
                Common.Permissions.Posts.Delete,
                
                Common.Permissions.Organizers.Read
            ]);
        private Role() { }
        private Role(Guid id, string name, string[] permissions) : base(id)
        { 
            RoleName = name;
            Permissions = permissions;

        }
        public string RoleName { get; private set; }
        public string[] Permissions { get; private set; }
    }
}
