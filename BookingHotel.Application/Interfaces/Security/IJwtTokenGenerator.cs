

namespace BookingHotel.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(
        string userId,
        string email,
        string firstName,
        string lastName,
        IList<string> roles,
        CancellationToken cancellationToken);
}