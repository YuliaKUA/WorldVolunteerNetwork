namespace WorldVolunteerNetwork.Application.Dtos
{
    public record OrganizerDto
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Patronymic,
        IReadOnlyList<OrganizerPhotoDto> Photos 
    );
}
