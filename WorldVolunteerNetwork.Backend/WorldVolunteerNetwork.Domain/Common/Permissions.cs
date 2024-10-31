namespace WorldVolunteerNetwork.Domain.Common
{
    public static class Permissions
    {
        public static class Organizers
        {
            public const string Read = "organizers.read";
            public const string Create = "organizers.create";
            public const string Update = "organizers.update";
            public const string Delete = "organizers.delete";

        }

        public static class Volunteers
        {
            public const string Read = "volunteer.read";
            public const string Create = "volunteer.create";
            public const string Update = "volunteer.update";
            public const string Delete = "volunteer.delete";

        }
        public static class Posts
        {
            public const string Read = "posts.read";
            public const string Create = "posts.create";
            public const string Update = "posts.update";
            public const string Delete = "posts.delete";

        }
        public static class VolunteerApplications
        {
            public const string Read = "volunteer.applications.read";
            public const string Create = "volunteer.applications.create";
            public const string Update = "volunteer.applications.update";
            public const string Delete = "volunteer.applications.delete";

        }
    }
}
