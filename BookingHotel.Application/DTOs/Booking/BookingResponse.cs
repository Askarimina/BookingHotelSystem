namespace BookingHotel.Application.DTOs.Booking;

public sealed record BookingResponse(
    Guid Id,
    Guid RoomId,
    string UserId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string Status,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);