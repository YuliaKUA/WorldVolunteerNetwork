using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Features.Posts
{
    public interface IPostsRepository
    {
        Task<Result<Post, Error>> GetById(Guid id);
    }
}
