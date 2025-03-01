namespace Portfolio.Application.DTOs;

public record UserResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}