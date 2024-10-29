using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.ReadModels
{
    public class OrganizerReadModel
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Patronymic { get; init; } = string.Empty;
        public string? Description { get; init; } = string.Empty;

        public int YearsVolunteeringExperience { get; init; }

        //public Account Account { get; private set; }

        public bool ActsBehalfCharitableOrganization { get; init; } = false;

        public List<PhotoReadModel> Photos { get; init; } = [];

        public List<SocialMediaReadModel> SocialMedias { get; init; } = [];

        public List<PostReadModel> Posts { get; init; } = [];
    }
}
