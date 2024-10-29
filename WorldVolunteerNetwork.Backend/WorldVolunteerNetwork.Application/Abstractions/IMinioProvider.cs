using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Abstractions
{
    public interface IMinioProvider
    {
        Task<Result<string, Error>> UploadPhoto(IFormFile photo, string path);
        Task<Result<bool, Error>> RemovePhoto(string path);
        Task<Result<IReadOnlyList<string>, Error>> GetPhotos(List<string> paths);
        Task<Result<IReadOnlyList<string>, Error>> GetPhotos(IEnumerable<string> paths);

    }
}
