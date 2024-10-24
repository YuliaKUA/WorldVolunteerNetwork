namespace WorldVolunteerNetwork.Application.Dtos;

public class PostDto
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string? Duration { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public float? Reward { get; init; }
    public DateTimeOffset SubmissionDeadline { get; init; }
    public DateTimeOffset DateCreate { get; init; }
    public List<OrganizerPhotoDto> Photos { get; init; } = [];
}