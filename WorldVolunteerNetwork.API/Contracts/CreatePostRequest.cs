using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.API.Contracts
{
    public partial class PostController
    {
        //DTO
        public record CreatePostRequest(
            string Name,
            string Location,
            float? Payment,
            float? Reward,
            string? Duration,
            string? Employment,
            string? Restriction,
            DateTime? SubmissionDeadline,
            string? Description

            );

        // public string Name {get; init;}
    }
}
