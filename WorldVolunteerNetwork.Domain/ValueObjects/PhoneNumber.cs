using System.Text.RegularExpressions;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record PhoneNumber
    {
        private const string russionPhoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
        public string Number { get; }
        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static PhoneNumber Create(string input)
        {
            if (input.IsEmpty())
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (Regex.IsMatch(input, russionPhoneRegex) == false)
            {
                throw new ArgumentException();
            }
            return new PhoneNumber(input);
        }
    }
}