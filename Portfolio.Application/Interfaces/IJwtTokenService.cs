using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateJwtToken(User user);
    string GenerateRefreshToken();
}