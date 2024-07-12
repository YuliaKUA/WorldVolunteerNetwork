namespace Contracts.Posts.Dtos
{
    public record PhotoDto(
        Guid Id,
        string Path,
        bool IsMain,
        Guid PostId
        );
}