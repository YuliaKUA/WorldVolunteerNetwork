namespace Contracts.Posts.Requests
{
    //DTO (Data Transfer Object )
    public record CreatePostRequest(
        Guid OrganizerId,

        string Name,

        string PostalCode,
        string Country,
        string City,
        string Street,
        string Building,

        string Duration,
        string Employment,
        string Restriction,
        string Description,

        string ContactNumber,

        string PostStatus,

        string Age,
        string Gender,

        float Reward,
        float Payment,

        DateTimeOffset SubmissionDeadline,
        DateTimeOffset DateCreate
    );

}
