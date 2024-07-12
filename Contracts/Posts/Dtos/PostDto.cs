namespace Contracts.Posts.Dtos
{
    public record PostDto(
        Guid Id,
        string Name,
        string? Duration,
        string? Description,
        string PostStatus,
        float? Reward,
        DateTimeOffset SubmissionDeadline,
        DateTimeOffset DateCreate //,
                                  // List<PhotoDto> Photos
        );
}