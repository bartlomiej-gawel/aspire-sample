using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Sample.Modules.Users.Features.Users;

namespace Sample.Modules.Users.Infrastructure.Jwt;

internal sealed class JwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ErrorOr<string> GenerateAccessToken(User user)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtSettings:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var jwtHandler = new JsonWebTokenHandler();

        var accessToken = jwtHandler.CreateToken(tokenDescriptor);
        if (string.IsNullOrEmpty(accessToken))
            return JwtErrors.AccessTokenGenerationFailed;

        return accessToken;
    }

    public ErrorOr<string> GenerateRefreshToken()
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        if (string.IsNullOrEmpty(refreshToken))
            return JwtErrors.RefreshTokenGenerationFailed;

        return refreshToken;
    }
}