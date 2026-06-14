using BookingHotel.Domain.Entities;

namespace BookingHotel.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Booking?> GetByIdWithRoomAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> HasOverlapAsync(
        Guid roomId,
        DateTime checkInDate,
        DateTime checkOutDate,
        CancellationToken cancellationToken);

    Task AddAsync(Booking booking, CancellationToken cancellationToken);

    void Update(Booking booking);

    //Task SaveChangesAsync(CancellationToken cancellationToken);
}