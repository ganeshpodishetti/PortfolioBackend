namespace Portfolio.Application.DTOs;

public record ForgotPasswordDto
{
    public required string Email { get; init; }
}