using BookingHotel.Application.DTOs.Auth;
using BookingHotel.Application.Interfaces.Security;
using BookingHotel.Application.Interfaces.Services;
using BookingHotel.Application.Services;
using BookingHotel.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Infrastructure.Identity;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            throw new InvalidOperationException("A user with this email already exists.");

        var user = new ApplicationUser(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Email);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }

        await _userManager.AddToRoleAsync(user, Roles.User);

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _jwtTokenGenerator.GenerateTokenAsync(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            roles,
            cancellationToken);

        return new AuthResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            token);
    }

    public async Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _jwtTokenGenerator.GenerateTokenAsync(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            roles,
            cancellationToken);

        return new AuthResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            token);
    }
}