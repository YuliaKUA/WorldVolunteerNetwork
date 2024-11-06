using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using WorldVolunteerNetwork.Domain.Common;
using ValueObject = WorldVolunteerNetwork.Domain.Common.ValueObject;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public static Result<Email, Error> Create(string input)
        {
            input = input.Trim();
            if (input.Length is < 1 or > Constraints.SHORT_TITLE_LENGTH)
                return Errors.General.InvalidLength("email");

            if (Regex.IsMatch(input, "^(.+)@(.+)$") == false)
                return Errors.General.ValueIsInvalid("email");

            return new Email(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
