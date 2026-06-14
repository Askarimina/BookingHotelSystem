using BookingHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHotel.Infrastructure.Persistence.Configurations;

public sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.HotelId)
            .IsRequired();

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.PricePerNight)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(r => r.CreatedAtUtc)
            .IsRequired();

        builder.Property(r => r.UpdatedAtUtc);

        builder.HasIndex(r => r.HotelId);

        builder.HasIndex(r => new { r.HotelId, r.RoomNumber })
            .IsUnique();

        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}