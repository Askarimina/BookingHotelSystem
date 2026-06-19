using BookingHotel.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Infrastructure.Persistence.Seed;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(
        RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(
                new IdentityRole(Roles.Admin));
        }

        if (!await roleManager.RoleExistsAsync(Roles.User))
        {
            await roleManager.CreateAsync(
                new IdentityRole(Roles.User));
        }
    }
}