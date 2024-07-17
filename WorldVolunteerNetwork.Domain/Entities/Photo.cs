namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Photo
    {
        private Photo() { }
        public Photo(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public Guid Id { get; private set; }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }
    }
}