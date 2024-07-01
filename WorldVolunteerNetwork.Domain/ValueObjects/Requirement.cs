using System.ComponentModel.DataAnnotations.Schema;
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
    }
}