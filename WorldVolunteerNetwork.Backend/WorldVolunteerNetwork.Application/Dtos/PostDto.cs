namespace WorldVolunteerNetwork.Application.Dtos;

public record PostDto (
    Guid Id, 
    string Name, 
    string? Duration, 
    string Description,
    string Status,
    float? Reward,
    DateTimeOffset SubmissionDeadline,
    DateTimeOffset DateCreate,
    List<OrganizerPhotoDto> Photos)
{}