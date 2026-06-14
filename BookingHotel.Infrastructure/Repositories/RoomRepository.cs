using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Domain.Entities;
using BookingHotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Infrastructure.Repositories;

public sealed class RoomRepository : IRoomRepository
{
    private readonly BookingHotelDbContext _context;

    public RoomRepository(BookingHotelDbContext context)
    {
        _context = context;
    }

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Rooms
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Rooms
            .AsNoTracking()
            .AnyAsync(r => r.Id == id, cancellationToken);
    }

    public Task<bool> ExistsInHotelAsync(
        Guid hotelId,
        string roomNumber,
        CancellationToken cancellationToken)
    {
        return _context.Rooms
            .AsNoTracking()
            .AnyAsync(
                r => r.HotelId == hotelId && r.RoomNumber == roomNumber,
                cancellationToken);
    }

    public async Task AddAsync(Room room, CancellationToken cancellationToken)
    {
        await _context.Rooms.AddAsync(room, cancellationToken);
    }

    public void Update(Room room)
    {
        _context.Rooms.Update(room);
    }

    public void Delete(Room room)
    {
        _context.Rooms.Remove(room);
    }

    //public Task SaveChangesAsync(CancellationToken cancellationToken)
    //{
    //    return _context.SaveChangesAsync(cancellationToken);
    //}
}