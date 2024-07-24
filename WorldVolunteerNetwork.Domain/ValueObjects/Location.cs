using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class Location : Common.ValueObject
    {
        public const int POSTAL_CODE_LENGTH = 6;
        public Location(string postalCode, string country, string city, string street, string building)
        {
            PostalCode = postalCode;
            Country = country;
            City = city;
            Street = street;
            Building = building;
        }

        public string PostalCode { get; }
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string Building { get; }

        public static Result<Location, Error> Create(string postalCode, string country, string city, string street, string building)
        {
            postalCode = postalCode.Trim();
            country = country.Trim();
            city = city.Trim();
            building = building.Trim();

            if (postalCode.Length != POSTAL_CODE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(postalCode));
            }
            if (country.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(country));
            }
            if (city.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(city));
            }
            if (street.Length is <Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(street));
            }
            if (building.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            {
                return Errors.General.ValueIsRequired(nameof(building));
            }

            return new Location(postalCode, country, city, street, building);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostalCode;
            yield return Country;
            yield return City;
            yield return Street;
            yield return Building;
        }
    }

}
