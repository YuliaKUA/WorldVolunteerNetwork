using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        //public PhoneNumber PhoneNumber { get; private set; }
    }
}
