namespace Portfolio.Application.DTOs;

public record LoginResponseDto
{
    public required string Email { get; init; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? RefreshTokenExpiresAtUtc { get; init; }
}