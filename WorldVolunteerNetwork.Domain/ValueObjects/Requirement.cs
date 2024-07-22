using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Requirement
    {
        public string? Age { get; private set; }
        public string? Gender { get; private set; }

        public Requirement(string age, string gender)
        {
            Age = age;
            Gender = gender;
        }

        public static Result<Requirement, Error> Create(string age, string gender)
        {
            if (age.IsEmpty())
            {
                return Errors.General.ValueIsRequired(nameof(age));
            }
            if (!int.TryParse(age, out int n))
            {
                return Errors.General.ItsNotNumber(nameof(age));
            }
            if (Convert.ToInt32(age) < Constraints.MINIMUM_AGE)
            {
                return Errors.General.ValueIsInvalid(nameof(age));
            }
            if (gender.IsEmpty())
            {
                return Errors.General.ValueIsRequired(nameof(gender));
            }

            return new Requirement(age, gender);
        }
    }
}