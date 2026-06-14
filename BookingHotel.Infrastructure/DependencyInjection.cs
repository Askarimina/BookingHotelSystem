


using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Infrastructure.Persistence;
using BookingHotel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingHotel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<BookingHotelDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
    }
}