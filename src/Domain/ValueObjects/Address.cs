public partial record Address
{
    public Address(string country, string line1, string line2, string city, string state, string zipCode)
    {
        Country = country;
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Country { get; set; } = null!;
    public string Line1 { get; set; } = null!;
    public string Line2 { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

    public static Address? Create(string country, string line1, string line2, string city, string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(country) ||
            string.IsNullOrWhiteSpace(line1) ||
            string.IsNullOrWhiteSpace(line2) ||
            string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(state) ||
            string.IsNullOrWhiteSpace(zipCode))
        {
            return null;
        }

        return new Address(country, line1, line2, city, state, zipCode);
    }
}