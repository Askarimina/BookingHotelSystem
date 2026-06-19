using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingHotel.Application.Interfaces.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookingHotel.Infrastructure.Identity;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(
        IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public Task<string> GenerateTokenAsync(
        string userId,
        string email,
        string firstName,
        string lastName,
        IList<string> roles,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, email),

            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Email, email),

            new("firstName", firstName),
            new("lastName", lastName)
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtSettings.Key));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpiryMinutes),
            signingCredentials: credentials);

        return Task.FromResult(
            new JwtSecurityTokenHandler()
                .WriteToken(token));
    }
}

//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using BookingHotel.Application.Interfaces.Security;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;

//namespace BookingHotel.Infrastructure.Identity;

//public sealed class JwtTokenGenerator : IJwtTokenGenerator
//{
//    private readonly IConfiguration _configuration;

//    public JwtTokenGenerator(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public Task<string> GenerateTokenAsync(
//        string userId,
//        string email,
//        string firstName,
//        string lastName,
//        IList<string> roles,
//        CancellationToken cancellationToken)
//    {
//        var jwtKey = _configuration["Jwt:Key"];
//        var issuer = _configuration["Jwt:Issuer"];
//        var audience = _configuration["Jwt:Audience"];

//        if (string.IsNullOrWhiteSpace(jwtKey))
//            throw new InvalidOperationException("JWT key is not configured.");

//        var claims = new List<Claim>
//        {
//            new(JwtRegisteredClaimNames.Sub, userId),
//            new(JwtRegisteredClaimNames.Email, email),
//            new(ClaimTypes.NameIdentifier, userId),
//            new(ClaimTypes.Email, email),
//            new(ClaimTypes.GivenName, firstName),
//            new(ClaimTypes.Surname, lastName)
//        };

//        foreach (var role in roles)
//        {
//            claims.Add(new Claim(ClaimTypes.Role, role));
//        }

//        var signingKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(jwtKey));

//        var credentials = new SigningCredentials(
//            signingKey,
//            SecurityAlgorithms.HmacSha256);

//        var token = new JwtSecurityToken(
//            issuer: issuer,
//            audience: audience,
//            claims: claims,
//            expires: DateTime.UtcNow.AddHours(2),
//            signingCredentials: credentials);

//        var tokenValue = new JwtSecurityTokenHandler()
//            .WriteToken(token);

//        return Task.FromResult(tokenValue);
//    }
//}