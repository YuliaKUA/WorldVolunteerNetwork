using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Requirement
    {
        public string? Age { get; private set; }
        public string? Gender { get; private set; }
        [NotMapped]
        public IReadOnlyList<Vaccination> Vaccinations => _vaccinations;
        private readonly List<Vaccination> _vaccinations = [];

        public Requirement(string age, string gender)
        {
            Age = age;
            Gender = gender;
        }

        public static Result<Requirement, Error> Create(string age, string gender)
        {
            if (age.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            if (gender.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }

            return new Requirement(age, gender);
        }
    }
}