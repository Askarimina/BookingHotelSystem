namespace BookingHotel.Application.DTOs.Room;

public sealed record RoomResponse(
    Guid Id,
    Guid HotelId,
    string RoomNumber,
    int Capacity,
    decimal PricePerNight,
    string Description,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);