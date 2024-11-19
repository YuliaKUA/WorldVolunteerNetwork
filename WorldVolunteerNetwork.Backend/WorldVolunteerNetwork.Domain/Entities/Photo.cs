using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using Entity = WorldVolunteerNetwork.Domain.Common.Entity;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public abstract class Photo : Entity
    {
        public const string JPEG = ".jpeg";
        public const string JPG = ".jpg";
        public const string PNG = ".png";

        protected Photo(string path, bool isMain)
        {
            Path = path;
            isMain = isMain;
        }

        public string Path { get; private set; }
        public bool IsMain { get; private set; }

        //public static Result<OrganizerPhoto, Error> CreateAndActivate(string path, string contentType, long length, bool isMain)
        //{
        //    if (contentType != JPEG && contentType != JPG && contentType != PNG)
        //        return Errors.Organizers.FileTypeInvalid(contentType);

        //    if (length > 10000)
        //        return Errors.Organizers.FileLengthInvalid(length);
        //    return OrganizerPhoto.Create(path, isMain);
        //}
    }
}
