using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class PostPhoto : Photo
    {
        public PostPhoto(string path, bool isMain) : base(path, isMain)
        {
           
        }

        public static Result<PostPhoto, Error> CreateAndActivaePostPhoto(
            string path, 
            string contentType, 
            long length, 
            bool isMain)
        {
            if (contentType != JPEG && contentType != JPG && contentType != PNG)
                return Errors.Organizers.FileTypeInvalid(contentType);

            if (length > 10000000)
                return Errors.Organizers.FileLengthInvalid(length);
            return new PostPhoto(path, isMain);
        }

        public static Result<PostPhoto, Error> Create(
            string contentType,
            long length)
        {
            if (contentType != JPEG && contentType != JPG && contentType != PNG)
                return Errors.Organizers.FileTypeInvalid(contentType);

            if (length > 10000000)
                return Errors.Organizers.FileLengthInvalid(length);

            var path = Guid.NewGuid() + contentType;
            return new PostPhoto(path, false);
        }
    }
}
