using BookingHotel.Application.DTOs.Auth;

namespace BookingHotel.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken);

    Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken);
}