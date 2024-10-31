using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Organizer : Common.Entity
    {
        public const int PHOTO_COUNT_LIMIT = 5;
        private Organizer() { }
        private Organizer(
            Guid userId,
            FullName fullName,
            string? description,
            int volunteeringExperience,
            bool actsBehalfCharitableOrganization,
            IEnumerable<SocialMedia> socialMedias
            //List<Photo> photos
            ) : base (userId)
        {
            FullName = fullName;
            Description = description;
            YearsVolunteeringExperience = volunteeringExperience;
            ActsBehalfCharitableOrganization = actsBehalfCharitableOrganization;
            _socialMedias = socialMedias.ToList();
            //_photos = photos;
        }

        public FullName FullName { get; private set; }
        public string? Description { get; private set; }

        public int YearsVolunteeringExperience { get; private set; }

        //public Account Account { get; private set; }

        public bool ActsBehalfCharitableOrganization { get; private set; } = false;

        
        private readonly List<OrganizerPhoto> _photos = [];
        public IReadOnlyList<OrganizerPhoto> Photos => _photos;


        private readonly List<SocialMedia> _socialMedias = [];
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;


        private readonly List<Post> _posts = [];
        public IReadOnlyList<Post> Posts => _posts;


        public void PublishPost(Post post)
        {
            _posts.Add(post);
        }

        public Result<bool, Error> AddPhoto(OrganizerPhoto photo)
        {
            if (_photos.Count >= PHOTO_COUNT_LIMIT)
                return Errors.Organizers.PhotoCountLimit(PHOTO_COUNT_LIMIT);
            
            _photos.Add(photo);
            return true;
        }

        public Result<bool, Error> DeletePhoto(string path)
        {
            var photo = _photos.FirstOrDefault(p => p.Path.Contains(path));
            if (photo is null)
                return Errors.Organizers.PhotoNotFound();

            _photos.Remove(photo);
            return true;
        }

        public static Result<Organizer, Error> Create(
            Guid userId,
            FullName name,
            string? description,
            int volunteeringExperience,
            bool actsBehalfCharitableOrganization,
            IEnumerable<SocialMedia> socialMedias
            )
        {
            if(userId == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(userId));
            
            if (description.IsEmpty())
            {
                return Errors.General.ValueIsRequired("organizer: description");
            }

            return new Organizer(
                userId,
                name,
                description,
                volunteeringExperience,
                actsBehalfCharitableOrganization, 
                socialMedias
                );
        }
    }
}
