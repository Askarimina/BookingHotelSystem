using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    private ApplicationUser()
    {
    }

    public ApplicationUser(
        string firstName,
        string lastName,
        string email,
        string userName)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        UserName = userName.Trim();
    }
}