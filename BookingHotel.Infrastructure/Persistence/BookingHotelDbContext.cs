using BookingHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Infrastructure.Persistence;

public sealed class BookingHotelDbContext : DbContext
{
    public BookingHotelDbContext(DbContextOptions<BookingHotelDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hotel> Hotels => Set<Hotel>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingHotelDbContext).Assembly);
    }
}