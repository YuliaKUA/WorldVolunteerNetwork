namespace WorldVolunteerNetwork.Application.Dtos
{
    public record PostPhotoDto(
        Guid Id,
        string Path,
        bool IsMain,
        Guid PostId
        //PostPhotoDto() { }
    );
}
