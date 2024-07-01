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

    }

}
