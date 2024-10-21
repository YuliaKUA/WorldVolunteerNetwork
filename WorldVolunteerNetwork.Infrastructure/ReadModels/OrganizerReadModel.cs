using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.ReadModels
{
    public class OrganizerReadModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; } = string.Empty;

        public int YearsVolunteeringExperience { get; init; }

        //public Account Account { get; private set; }

        public bool ActsBehalfCharitableOrganization { get; init; } = false;

        public IReadOnlyList<PhotoReadModel> Photos { get; init; } = [];

        public IReadOnlyList<SocialMediaReadModel> SocialMedias { get; init; } = [];

        public IReadOnlyList<PostReadModel> Posts { get; init; } = [];
    }
}
