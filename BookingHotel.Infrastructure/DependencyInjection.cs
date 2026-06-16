using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Application.Interfaces.Security;
using BookingHotel.Application.Services;
using BookingHotel.Infrastructure.Identity;
using BookingHotel.Infrastructure.Persistence;
using BookingHotel.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookingHotel.Application.Interfaces.Services;

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

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<BookingHotelDbContext>()
            .AddDefaultTokenProviders();

        var jwtKey = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

        if (string.IsNullOrWhiteSpace(jwtKey))
            throw new InvalidOperationException("JWT key is not configured.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                    };
            });

        services.AddAuthorization();

        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}





//using BookingHotel.Application.Interfaces.Repositories;
//using BookingHotel.Infrastructure.Persistence;
//using BookingHotel.Infrastructure.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace BookingHotel.Infrastructure;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddInfrastructure(
//        this IServiceCollection services,
//        IConfiguration configuration)
//    {
//        var connectionString = configuration.GetConnectionString("DefaultConnection");

//        services.AddDbContext<BookingHotelDbContext>(options =>
//        {
//            options.UseSqlServer(connectionString);
//        });

//        services.AddScoped<IHotelRepository, HotelRepository>();
//        services.AddScoped<IRoomRepository, RoomRepository>();
//        services.AddScoped<IBookingRepository, BookingRepository>();

//        services.AddScoped<IUnitOfWork, UnitOfWork>();


//        return services;
//    }
//}