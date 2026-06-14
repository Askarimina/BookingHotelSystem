using BookingHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHotel.Infrastructure.Persistence.Configurations;

public sealed class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotels");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(h => h.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(h => h.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(h => h.CreatedAtUtc)
            .IsRequired();

        builder.Property(h => h.UpdatedAtUtc);

        builder.HasIndex(h => h.City);

        builder.HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}