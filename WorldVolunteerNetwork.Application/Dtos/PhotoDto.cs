namespace WorldVolunteerNetwork.Application.Dtos;

public class PhotoDto
{
    public Guid Id { get; init; }
    public string Path { get; init; } = string.Empty;
    public bool IsMain { get; init; }
    public Guid PostId { get; init; }
}