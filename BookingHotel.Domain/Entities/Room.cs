namespace BookingHotel.Domain.Entities;

public sealed class Room : BaseEntity
{
    private readonly List<Booking> _bookings = new();

    private Room()
    {
    }

    public Room(
        Guid hotelId,
        string roomNumber,
        int capacity,
        decimal pricePerNight,
        string description)
    {
        HotelId = hotelId;
        SetRoomNumber(roomNumber);
        SetCapacity(capacity);
        SetPricePerNight(pricePerNight);
        SetDescription(description);
    }

    public Guid HotelId { get; private set; }

    public Hotel? Hotel { get; private set; }

    public string RoomNumber { get; private set; } = string.Empty;

    public int Capacity { get; private set; }

    public decimal PricePerNight { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

    public void Update(
        string roomNumber,
        int capacity,
        decimal pricePerNight,
        string description)
    {
        SetRoomNumber(roomNumber);
        SetCapacity(capacity);
        SetPricePerNight(pricePerNight);
        SetDescription(description);
        MarkAsUpdated();
    }

    private void SetRoomNumber(string roomNumber)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new ArgumentException("Room number is required.", nameof(roomNumber));

        if (roomNumber.Length > 50)
            throw new ArgumentException("Room number cannot exceed 50 characters.", nameof(roomNumber));

        RoomNumber = roomNumber.Trim();
    }

    private void SetCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

        if (capacity > 20)
            throw new ArgumentException("Capacity cannot exceed 20.", nameof(capacity));

        Capacity = capacity;
    }

    private void SetPricePerNight(decimal pricePerNight)
    {
        if (pricePerNight <= 0)
            throw new ArgumentException("Price per night must be greater than zero.", nameof(pricePerNight));

        PricePerNight = pricePerNight;
    }

    private void SetDescription(string description)
    {
        if (description.Length > 1000)
            throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(description));

        Description = description.Trim();
    }
}