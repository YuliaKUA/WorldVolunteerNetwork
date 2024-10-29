using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class OrganizerPhoto : Common.Entity
    {
        private protected OrganizerPhoto() { }
        private protected OrganizerPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public string Path { get; private set; }
        public bool IsMain { get; private set; }

        public static Result<OrganizerPhoto, Error> Create(
            string path,
            bool isMain)
        {
            if (path.IsEmpty())
            {
                return Errors.General.ValueIsRequired("photo: path");
            }

            return new OrganizerPhoto(path, isMain);
        }
    }
}
