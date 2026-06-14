using BookingHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHotel.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.RoomId)
            .IsRequired();

        builder.Property(b => b.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(b => b.CheckInDate)
            .IsRequired();

        builder.Property(b => b.CheckOutDate)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.CreatedAtUtc)
            .IsRequired();

        builder.Property(b => b.UpdatedAtUtc);

        builder.HasIndex(b => b.RoomId);

        builder.HasIndex(b => b.UserId);

        builder.HasIndex(b => new { b.RoomId, b.CheckInDate, b.CheckOutDate });

        builder.HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}