namespace BookingHotel.Application.DTOs.Room;

public sealed record UpdateRoomRequest(
    string RoomNumber,
    int Capacity,
    decimal PricePerNight,
    string Description);