namespace WorldVolunteerNetwork.Application.Dtos;

public class PostDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Duration { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public float? Reward { get; set; }
    public DateTimeOffset SubmissionDeadline { get; set; }
    public DateTimeOffset DateCreate { get; set; }
    public List<PostPhotoDto> Photos = [];
    public PostDto() { }
    public PostDto(
        Guid Id,
        string Name,
        string? Duration,
        string Description,
        string Status,
        float? Reward,
        DateTimeOffset SubmissionDeadline,
        DateTimeOffset DateCreate,
        List<PostPhotoDto> Photos)
    { }
}