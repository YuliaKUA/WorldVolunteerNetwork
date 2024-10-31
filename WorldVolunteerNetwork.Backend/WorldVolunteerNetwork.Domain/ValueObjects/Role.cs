using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Role : ValueObject
    {
        public static readonly Role Admin = new Role(
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
            nameof(Organizer).ToUpper(),
            [
                Common.Permissions.Posts.Create,
                Common.Permissions.Posts.Read,
                Common.Permissions.Posts.Update,
                Common.Permissions.Posts.Delete,

                Common.Permissions.Organizers.Read
            ]);

        public static readonly Role Volunteer = new Role(
            nameof(Volunteer).ToUpper(),
            [
                Common.Permissions.Posts.Create,
                Common.Permissions.Posts.Read,
                Common.Permissions.Posts.Update,
                Common.Permissions.Posts.Delete,

                Common.Permissions.Organizers.Read,

                Common.Permissions.Volunteers.Read
            ]);

        public static readonly Role RegularUser = new Role(
            nameof(Volunteer).ToUpper(),
            [
                Common.Permissions.Posts.Read,
                Common.Permissions.Organizers.Read,
                Common.Permissions.Volunteers.Read
            ]);
        private Role(string name, string[] permissions)
        {
            RoleName = name;
            Permissions = permissions;

        }
        public Role() { }
        public string RoleName { get; }
        public string[] Permissions { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return RoleName;
        }
    }
}
