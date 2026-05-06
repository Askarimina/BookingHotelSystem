namespace BookingHotel.Domain.Entities;

public sealed class Hotel : BaseEntity
{
    private readonly List<Room> _rooms = new();

    private Hotel()
    {
    }

    public Hotel(string name, string city, string address, string description)
    {
        SetName(name);
        SetCity(city);
        SetAddress(address);
        SetDescription(description);
    }

    public string Name { get; private set; } = string.Empty;

    public string City { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    public void Update(string name, string city, string address, string description)
    {
        SetName(name);
        SetCity(city);
        SetAddress(address);
        SetDescription(description);
        MarkAsUpdated();
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hotel name is required.", nameof(name));

        if (name.Length > 150)
            throw new ArgumentException("Hotel name cannot exceed 150 characters.", nameof(name));

        Name = name.Trim();
    }

    private void SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.", nameof(city));

        if (city.Length > 100)
            throw new ArgumentException("City cannot exceed 100 characters.", nameof(city));

        City = city.Trim();
    }

    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));

        if (address.Length > 250)
            throw new ArgumentException("Address cannot exceed 250 characters.", nameof(address));

        Address = address.Trim();
    }

    private void SetDescription(string description)
    {
        if (description.Length > 1000)
            throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(description));

        Description = description.Trim();
    }
}