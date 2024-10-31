
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class User : Entity
    {
        private User() { }
        public User(Email email, string passwordHash, Role role) 
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }
        public Email Email {  get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }

    }
}
