using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Domain.Entities;
using BookingHotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Infrastructure.Repositories;

public sealed class HotelRepository : IHotelRepository
{
    private readonly BookingHotelDbContext _context;

    public HotelRepository(BookingHotelDbContext context)
    {
        _context = context;
    }

    public Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Hotels
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Hotels
            .AsNoTracking()
            .AnyAsync(h => h.Id == id, cancellationToken);
    }

    public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken)
    {
        await _context.Hotels.AddAsync(hotel, cancellationToken);
    }

    public void Update(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
    }

    public void Delete(Hotel hotel)
    {
        _context.Hotels.Remove(hotel);
    }

    //public Task SaveChangesAsync(CancellationToken cancellationToken)
    //{
    //    return _context.SaveChangesAsync(cancellationToken);
    //}
}