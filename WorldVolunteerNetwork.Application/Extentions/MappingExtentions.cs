using Contracts.Posts.Dtos;
using WorldVolunteerNetwork.Domain.Entities;


namespace WorldVolunteerNetwork.Application.Mapping
{
    public static class MappingExtentions
    {
        public static PostDto ToDto(this Post p)
        {
            return new PostDto(
                p.Id,
                p.Name,
                p.Duration,
                p.Description,
                p.Status.Value,
                p.Reward,
                p.SubmissionDeadline,
                p.DateCreate,
                p.Photos);
        }
    }
}
