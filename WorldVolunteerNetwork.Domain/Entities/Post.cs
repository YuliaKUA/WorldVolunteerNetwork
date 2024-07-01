using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public partial class Post
    {
        public const int MAX_PROPERTY_LENGHT = 500;
        private Post() { }

        public Post(

            string name,
            Location location,
            float? payment,
            float? reward,
            string? duration,
            string? employment,
            string? restriction,
            DateTime? submissionDeadline,
            string? description,
            PhoneNumber contactNumber,
            Status status,
            Requirement requirement,
            List<Photo> photos)
        {

            Name = name;
            Location = location;
            Payment = payment;
            Reward = reward;
            Duration = duration;
            Employment = employment;
            Restriction = restriction;
            SubmissionDeadline = submissionDeadline;
            Description = description;
            ContactNumber = contactNumber;
            Status = status;
            Requirement = requirement;
            _photos = photos;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Location Location { get; private set; }
        public float? Payment { get; private set; }
        public float? Reward { get; private set; }
        public string? Duration { get; private set; }
        public string? Employment { get; private set; }
        public string? Restriction { get; private set; }
        public DateTime? SubmissionDeadline { get; private set; }
        public string? Description { get; private set; }
        public PhoneNumber ContactNumber { get; private set; }
        public Status Status { get; private set; }
        public DateTime DateCreate { get; private set; } = DateTime.Now;
        public Requirement Requirement { get; private set; }

        public IReadOnlyList<Photo> Photos => _photos;
        private readonly List<Photo> _photos = [];
    }
}
