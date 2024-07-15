using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class SocialMedia
    {
        private SocialMedia() { }
        private SocialMedia(
            string link,
            Social social)
        {
            Link = link;
            Social = social;
        }

        public Guid Id { get; set; }
        public string Link { get; set; }
        public Social Social { get; set; }

        public static Result<SocialMedia, Error> Create(
            string link,
            Social social)
        {
            if (link.IsEmpty())
            {
                return Errors.General.ValueIsRequired("social media: link");
            }

            return new SocialMedia(link, social);
        }
    }
}
