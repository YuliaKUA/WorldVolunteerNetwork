
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Role : Entity
    {
        public static Role Admin = new Role(nameof(Admin).ToUpper(), [
                "volunteer.application.read",
                "volunteer.application.update",

                "organizer.create",

                "post.read",
                "post.delete",

                "organizers.read",
                "organizers.delete"
            ]);

        public static Role Organizer = new Role(nameof(Organizer).ToUpper(), [
                "post.read",
                "post.create",
                "post.update",
                "post.delete",

                "organizers.read",
            ]);
        private Role() { }
        private Role(string name, string[] permissions)
        {
            RoleName = name;
            Permissions = permissions;

        }
        public string RoleName { get; private set; }
        public string[] Permissions { get; private set; }
    }
}
