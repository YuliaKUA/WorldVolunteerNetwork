using Microsoft.AspNetCore.Http;

namespace WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto
{
    public record UploadOrganizerPhotoRequest(
        Guid OrganizerId, 
        IFormFile File,
        bool isMain)
    {

    }
}
