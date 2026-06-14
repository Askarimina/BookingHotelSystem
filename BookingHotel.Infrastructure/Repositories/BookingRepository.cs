using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Domain.Constants;
using BookingHotel.Domain.Entities;
using BookingHotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Infrastructure.Repositories;

public sealed class BookingRepository : IBookingRepository
{
    private readonly BookingHotelDbContext _context;

    public BookingRepository(BookingHotelDbContext context)
    {
        _context = context;
    }

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<Booking?> GetByIdWithRoomAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Bookings
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<bool> HasOverlapAsync(
        Guid roomId,
        DateTime checkInDate,
        DateTime checkOutDate,
        CancellationToken cancellationToken)
    {
        return _context.Bookings
            .AsNoTracking()
            .AnyAsync(
                b =>
                    b.RoomId == roomId &&
                    b.Status != BookingStatuses.Cancelled &&
                    checkInDate < b.CheckOutDate &&
                    checkOutDate > b.CheckInDate,
                cancellationToken);
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken)
    {
        await _context.Bookings.AddAsync(booking, cancellationToken);
    }

    public void Update(Booking booking)
    {
        _context.Bookings.Update(booking);
    }

    //public Task SaveChangesAsync(CancellationToken cancellationToken)
    //{
    //    return _context.SaveChangesAsync(cancellationToken);
    //}
}