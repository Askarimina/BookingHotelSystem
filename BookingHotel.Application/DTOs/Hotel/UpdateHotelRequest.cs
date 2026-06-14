namespace BookingHotel.Application.DTOs.Hotel;

public sealed record UpdateHotelRequest(
    string Name,
    string City,
    string Address,
    string Description);
