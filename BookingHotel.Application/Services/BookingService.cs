using BookingHotel.Application.DTOs.Booking;
using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Application.Interfaces.Services;
using BookingHotel.Domain.Entities;
using BookingHotel.Domain.Exceptions;

namespace BookingHotel.Application.Services;

public sealed class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookingResponse> CreateAsync(
        CreateBookingRequest request,
        string userId,
        CancellationToken cancellationToken)
    {
        var roomExists = await _roomRepository.ExistsAsync(
            request.RoomId,
            cancellationToken);

        if (!roomExists)
            throw new KeyNotFoundException("Room was not found.");

        var hasOverlap = await _bookingRepository.HasOverlapAsync(
            request.RoomId,
            request.CheckInDate,
            request.CheckOutDate,
            cancellationToken);

        if (hasOverlap)
            throw new BookingConflictException();

        var booking = new Booking(
            request.RoomId,
            userId,
            request.CheckInDate,
            request.CheckOutDate);

        await _bookingRepository.AddAsync(booking, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(booking);
    }

    public async Task<BookingResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, cancellationToken);

        return booking is null ? null : MapToResponse(booking);
    }

    public async Task CancelAsync(
        Guid id,
        string userId,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, cancellationToken);

        if (booking is null)
            throw new KeyNotFoundException("Booking was not found.");

        if (booking.UserId != userId)
            throw new UnauthorizedAccessException("You are not allowed to cancel this booking.");

        booking.Cancel();

        _bookingRepository.Update(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static BookingResponse MapToResponse(Booking booking)
    {
        return new BookingResponse(
            booking.Id,
            booking.RoomId,
            booking.UserId,
            booking.CheckInDate,
            booking.CheckOutDate,
            booking.Status,
            booking.CreatedAtUtc,
            booking.UpdatedAtUtc);
    }
}
