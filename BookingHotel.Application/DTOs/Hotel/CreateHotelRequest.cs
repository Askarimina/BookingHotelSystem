namespace BookingHotel.Application.DTOs.Hotel;

public sealed record CreateHotelRequest(
    string Name,
    string City,
    string Address,
    string Description);