namespace Portfolio.Application.DTOs;

public record LoginRequestDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}