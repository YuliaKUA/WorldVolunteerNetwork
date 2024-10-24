namespace WorldVolunteerNetwork.Application.Dtos;

public class OrganizerPhotoDto
{
    public Guid Id { get; init; }
    public string Path { get; init; } = string.Empty;
    public bool IsMain { get; init; }
    public Guid OrganizerId { get; init; } = Guid.Empty;
}