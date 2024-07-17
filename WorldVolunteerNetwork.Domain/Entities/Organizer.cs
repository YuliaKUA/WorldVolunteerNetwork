using CSharpFunctionalExtensions;
using System.Collections.Generic;
using WorldVolunteerNetwork.Domain.Common;
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
            bool actsBehalfCharitableOrganization,
            IEnumerable<SocialMedia> socialMedias
            //List<Photo> photos
            )
        {
            Name = name;
            Description = description;
            YearsVolunteeringExperience = volunteeringExperience;
            ActsBehalfCharitableOrganization = actsBehalfCharitableOrganization;
            _socialMedias = socialMedias.ToList();
            //_photos = photos;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public int YearsVolunteeringExperience { get; private set; }

        //public Account Account { get; private set; }

        public bool ActsBehalfCharitableOrganization { get; private set; } = false;

        
        private readonly List<Photo> _photos = [];
        public IReadOnlyList<Photo> Photos => _photos;


        private readonly List<SocialMedia> _socialMedias = [];
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;


        private readonly List<Post> _posts = [];
        public IReadOnlyList<Post> Posts => _posts;


        public void PublishPost(Post post)
        {
            _posts.Add(post);
        }

        public static Result<Organizer, Error> Create(
            string name,
            string? description,
            int volunteeringExperience,
            bool actsBehalfCharitableOrganization,
            IEnumerable<SocialMedia> socialMedias
            )
        {
            if (name.IsEmpty())
            {
                return Errors.General.ValueIsRequired("organizer: name");
            }
            if (description.IsEmpty())
            {
                return Errors.General.ValueIsRequired("organizer: description");
            }

            return new Organizer(
                name,
                description,
                volunteeringExperience,
                actsBehalfCharitableOrganization, 
                socialMedias
                );
        }
    }
}
