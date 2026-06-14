namespace BookingHotel.Application.DTOs.Room;

public sealed record CreateRoomRequest(
    Guid HotelId,
    string RoomNumber,
    int Capacity,
    decimal PricePerNight,
    string Description);