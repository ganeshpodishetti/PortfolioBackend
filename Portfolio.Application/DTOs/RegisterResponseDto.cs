namespace Portfolio.Application.DTOs;

public record RegisterResponseDto
{
    public required string Email { get; init; }
    public string? CreateAt { get; set; }
}