
using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;
using Entity = WorldVolunteerNetwork.Domain.Common.Entity;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class User : Entity
    {
        private User() { }
        private User(Email email, string passwordHash, Role role) 
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }
        public Email Email {  get; private set; }
        public string PasswordHash { get; private set; }
        //public string[] Permissions { get; private set; }
        public Role Role { get; private set; }

        public static Result<User, Error> CreateAdmin(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.Admin);
        }

        public static Result<User, Error> CreateOrganizer(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.Organizer);
        }
        public static Result<User, Error> CreateVolunteer(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.Volunteer);
        }

        public static Result<User, Error> CreateRegularUser(Email email, string passwordHash)
        {
            return new User(email, passwordHash, Role.RegularUser);
        }

    }
}
