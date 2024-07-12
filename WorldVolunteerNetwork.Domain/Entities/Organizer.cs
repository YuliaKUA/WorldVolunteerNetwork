using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Organizer
    {
        private Organizer() { }
        private Organizer(
            string name,
            string? description,
            int volunteeringExperience,
            PhoneNumber phoneNumber,
            bool actsBehalfCharitableOrganization,
            Photo mainPhoto)
        {
            Name = name;
            Description = description;
            VolunteeringExperience = volunteeringExperience;
            PhoneNumber = phoneNumber;
            ActsBehalfCharitableOrganization = actsBehalfCharitableOrganization;
            MainPhoto = mainPhoto;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public int VolunteeringExperience { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }
        public Photo MainPhoto { get; private set; }
        public Account Account { get; private set; }

        public bool ActsBehalfCharitableOrganization { get; private set; } = false;


        private readonly List<SocialMedia> _socialMedias = [];
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;


        private readonly List<Post> _posts = [];
        public IReadOnlyList<Post> Posts => _posts;


        public void PublishPost(Post post)
        {
            _posts.Add(post);
        }
    }
}
