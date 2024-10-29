using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class PostPhoto : Common.Entity
    {
        private protected PostPhoto() { }
        private protected PostPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }

        public static Result<PostPhoto, Error> Create(
            string path,
            bool isMain)
        {
            if (path.IsEmpty())
            {
                return Errors.General.ValueIsRequired("photo: path");
            }

            return new PostPhoto(path, isMain);
        }
    }
}
