using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class FullName : Common.ValueObject
    {
        public FullName() { }
        private FullName(string firstName, string lastName, string? patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public string FirstName {get; }
        public string LastName {get; }
        public string? Patronymic { get; }

        public static Result<FullName, Error> Create(
            string firstName,
            string lastName,
            string patronymic)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            patronymic = patronymic?.Trim();

            if (firstName.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(firstName));
            }
            if (lastName.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(lastName));
            }
            if (patronymic?.Length is > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.InvalidLength(nameof(patronymic));
            }

            return new FullName(firstName, lastName, patronymic);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
