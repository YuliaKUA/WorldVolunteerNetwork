using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record Location
    {
        public const int MIN_PROPERTY_LENGHT = 1;
        public const int MAX_PROPERTY_LENGHT = 100;
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
            street = street.Trim();
            building = building.Trim();

            if (postalCode.Length is < MIN_PROPERTY_LENGHT or > MAX_PROPERTY_LENGHT)
            {
                return Errors.General.ValueIsRequired("postal code");
            }
            if (country.Length is < MIN_PROPERTY_LENGHT or > MAX_PROPERTY_LENGHT)
            {
                return Errors.General.ValueIsRequired("country");
            }
            if (city.Length is < MIN_PROPERTY_LENGHT or > MAX_PROPERTY_LENGHT)
            {
                return Errors.General.ValueIsRequired("city");
            }
            if (street.Length is < MIN_PROPERTY_LENGHT or > MAX_PROPERTY_LENGHT)
            {
                return Errors.General.ValueIsRequired("street");
            }
            if (building.Length is < MIN_PROPERTY_LENGHT or > MAX_PROPERTY_LENGHT)
            {
                return Errors.General.ValueIsRequired("building");
            }

            return new Location(postalCode, country, city, street, building);
        }
    }

}
