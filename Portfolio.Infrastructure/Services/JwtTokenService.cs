using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Domain.Contracts;
using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces;

namespace Portfolio.Infrastructure.Services;

internal class JwtTokenService(IOptions<JwtSettings> jwtOptions, 
    UserManager<User> userManager) 
    : IJwtTokenService
{
    // Generate JWT token
    public async Task<string> GenerateJwtToken(User user)
    {
        var jwtSettings = jwtOptions.Value;
        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
        {
            throw new InvalidOperationException("JWT secret key is not configured.");
        }
        
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key));
        
        var singingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var claims = await GetClaimsAsync(user);
        var tokenOptions = GenerateTokenOptions(singingCredentials, claims);
        
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return jwtToken;
    }

    // Generate Refresh Token
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    // Get Claims
    private async Task<List<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user?.UserName ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? string.Empty),
            new Claim(ClaimTypes.Email, user?.Email ?? string.Empty),
            new Claim("FirstName", user?.FirstName ?? string.Empty),
            new Claim("LastName", user?.LastName ?? string.Empty)
        };

        var roles =  await userManager.GetRolesAsync(user!);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
    
    // Generate Token Options
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: jwtOptions.Value.ValidIssuer,
            audience: jwtOptions.Value.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiresInMinutes),
            signingCredentials: signingCredentials
        );
    }
}