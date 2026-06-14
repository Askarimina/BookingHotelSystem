using BookingHotel.Application.Interfaces.Services;
using BookingHotel.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingHotel.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IBookingService, BookingService>();

        return services;
    }
}