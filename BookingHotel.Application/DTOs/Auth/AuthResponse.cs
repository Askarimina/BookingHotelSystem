namespace BookingHotel.Application.DTOs.Auth;

public sealed record AuthResponse(
    string UserId,
    string Email,
    string FirstName,
    string LastName,
    string Token);