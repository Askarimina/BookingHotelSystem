using BookingHotel.Application.DTOs.Booking;

namespace BookingHotel.Application.Interfaces.Services;

public interface IBookingService
{
    Task<BookingResponse> CreateAsync(
        CreateBookingRequest request,
        string userId,
        CancellationToken cancellationToken);

    Task<BookingResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task CancelAsync(
        Guid id,
        string userId,
        CancellationToken cancellationToken);
}