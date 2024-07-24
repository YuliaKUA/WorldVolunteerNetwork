using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class SocialMedia : Common.ValueObject
    {
        private SocialMedia(
            string link,
            Social social)
        {
            Link = link;
            Social = social;
        }

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Link;
            yield return Social;
        }
    }
}
