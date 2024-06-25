using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Post
    {
        public Post(
            string name,
            string? location,
            float? payment,
            float? reward,
            string? duration,
            string? employment,
            string? restriction,
            DateTime? submissionDeadline,
            string? description,
            string? photo)
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
            Photo = photo;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Location { get; private set; }
        public float? Payment { get; private set; }
        public float? Reward { get; private set; }
        public string? Duration { get; private set; }
        public string? Employment { get; private set; }
        public string? Restriction { get; private set; }
        public DateTime? SubmissionDeadline { get; private set; }
        public string? Description { get; private set; }
        public string? Photo { get; private set; }
    }
}
