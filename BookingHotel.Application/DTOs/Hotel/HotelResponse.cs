namespace BookingHotel.Application.DTOs.Hotel;

public sealed record HotelResponse(
    Guid Id,
    string Name,
    string City,
    string Address,
    string Description,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);