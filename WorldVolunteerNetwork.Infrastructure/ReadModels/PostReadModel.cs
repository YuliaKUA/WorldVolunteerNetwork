using WorldVolunteerNetwork.Application.Dtos;

namespace WorldVolunteerNetwork.Infrastructure.ReadModels
{
    public class PostReadModel
    {
        public Guid Id { get; init; }
        public Guid OrganizerId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Duration { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public float? Reward { get; init; }
        public DateTimeOffset SubmissionDeadline { get; init; }
        public DateTimeOffset DateCreate { get; init; }
        public List<PhotoReadModel> Photos { get; init; } = [];
    }
}
