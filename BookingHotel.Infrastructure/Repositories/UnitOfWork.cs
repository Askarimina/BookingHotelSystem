using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Infrastructure.Persistence;

namespace BookingHotel.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BookingHotelDbContext _context;

    public UnitOfWork(BookingHotelDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}