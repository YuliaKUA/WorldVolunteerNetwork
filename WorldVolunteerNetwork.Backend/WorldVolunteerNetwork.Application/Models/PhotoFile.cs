using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Models
{
    public record PhotoFile(
        PostPhoto PostPhoto,
        IFormFile File);
}