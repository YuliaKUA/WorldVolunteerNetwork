using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Social : Common.ValueObject
    {
        public static readonly Social Telegram = new(nameof(Telegram));
        public static readonly Social Instagram = new(nameof(Instagram));
        public static readonly Social WhatsApp = new(nameof(WhatsApp));
        public static readonly Social VK = new(nameof(VK));
        public static readonly Social FaceBook = new(nameof(FaceBook));
        public static readonly Social YouTube = new(nameof(YouTube));

        private static readonly Social[] _all = [Telegram, Instagram, WhatsApp, VK, FaceBook, YouTube];
        public string Value { get; }

        public Social() { }
        private Social(string value)
        {
            Value = value;
        }
        public static Result<Social, Error> Create(string input)
        {
            if (input.IsEmpty())
            {
                return Errors.General.ValueIsRequired("social");
            }

            var social = input.Trim().ToUpper();

            if (_all.Any(p => p.Value.ToUpper() == social) == false)
            {
                return Errors.General.ValueIsInvalid("social");
            }

            return new Social(social);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
