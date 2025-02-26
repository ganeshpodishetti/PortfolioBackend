using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class SocialMedia
{
    public string Id { get; set; }
    public SocialMediaName Name { get; set; }
    public string Url { get; set; } = null!;
    public string? Icon { get; set; } 
    
    public User User { get; init; } = null!;
}