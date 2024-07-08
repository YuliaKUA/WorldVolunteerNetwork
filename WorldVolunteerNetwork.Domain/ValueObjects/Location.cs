using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record Location
    {
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
            if (postalCode.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            if (country.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            if (city.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            if (street.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            if (building.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }

            return new Location(postalCode, country, city, street, building);
        }
    }

}
