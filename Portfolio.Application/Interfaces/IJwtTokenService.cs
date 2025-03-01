using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IJwtTokenService
{
    (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user);
    string GenerateRefreshToken();
}