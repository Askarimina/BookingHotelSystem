namespace BookingHotel.Application.DTOs.Booking;

public sealed record CreateBookingRequest(
    Guid RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate);