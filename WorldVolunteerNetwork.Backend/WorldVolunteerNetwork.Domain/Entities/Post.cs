using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public partial class Post : Common.Entity
    {
        public const int MAX_PROPERTY_LENGHT = 500;
        public const int MAX_NAME_LENGHT = 250;
        private Post() { }

        private Post(
            string name,
            string? duration,
            string? employment,
            string? restriction,
            string? description,
            float? payment,
            float? reward,
            Location location,
            PhoneNumber contactNumber,
            PostStatus status,
            Requirement requirement,
            DateTimeOffset submissionDeadline,
            DateTimeOffset dateCreate
            )
        {

            Name = name;
            Duration = duration;
            Employment = employment;
            Restriction = restriction;
            Description = description;
            Location = location;
            ContactNumber = contactNumber;
            Status = status;
            Requirement = requirement;
            Payment = payment;
            Reward = reward;
            SubmissionDeadline = submissionDeadline;
            DateCreate = dateCreate;
        }

        public string Name { get; private set; } = string.Empty;
        public string? Duration { get; private set; }
        public string? Employment { get; private set; }
        public string? Restriction { get; private set; }
        public string? Description { get; private set; }

        public Location Location { get; private set; }
        public PhoneNumber ContactNumber { get; private set; }
        public PostStatus Status { get; private set; }
        public Requirement Requirement { get; private set; }

        public float? Payment { get; private set; }
        public float? Reward { get; private set; }

        public DateTimeOffset SubmissionDeadline { get; private set; }
        public DateTimeOffset DateCreate { get; private set; }

        public IReadOnlyList<PostPhoto> Photos => _photos;
        private readonly List<PostPhoto> _photos = [];
        public IReadOnlyList<Vaccination> Vaccinations => _vaccinations;
        private readonly List<Vaccination> _vaccinations = [];


        public static Result<Post, Error> Create(
            string name,
            string? duration,
            string? employment,
            string? restriction,
            string? description,
            float? payment,
            float? reward,
            Location location,
            PhoneNumber contactNumber,
            PostStatus status,
            Requirement requirement,
            DateTimeOffset submissionDeadline,
            DateTimeOffset dateCreate
            )
        {
            if (name.IsEmpty())
            {
                return Errors.General.ValueIsRequired("post: name");
            }
            if (name.Length > MAX_NAME_LENGHT)
            {
                return Errors.General.InvalidLength();
            }
            if (duration.IsEmpty())
            {
                return Errors.General.ValueIsRequired("post: duration");
            }
            if (employment.IsEmpty())
            {
                return Errors.General.ValueIsRequired("post: employment");
            }
            if (restriction.IsEmpty())
            {
                return Errors.General.ValueIsRequired("post: restriction");
            }
            if (description.IsEmpty())
            {
                return Errors.General.ValueIsRequired("post: description");
            }

            return new Post(
                name,
                duration,
                employment,
                restriction,
                description,
                payment,
                reward,
                location,
                contactNumber,
                status,
                requirement,
                submissionDeadline,
                dateCreate
                );
        }
    }
}
