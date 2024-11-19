namespace WorldVolunteerNetwork.Infrastructure.ReadModels
{
    public class PostPhotoReadModel
    {
        public Guid Id { get; init; }
        public string Path { get; init; } = string.Empty;
        public bool IsMain { get; init; }

        public Guid PostId { get; init; }
    }
}
