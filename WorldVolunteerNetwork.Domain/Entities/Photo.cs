using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Photo
    {
        private Photo() { }
        private Photo(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public Guid Id { get; private set; }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }

        public static Result<Photo, Error> Create(
            string path,
            bool isMain)
        {
            if (path.IsEmpty())
            {
                return Errors.General.ValueIsRequired("photo: path");
            }

            return new Photo(path, isMain);
        }
    }
}