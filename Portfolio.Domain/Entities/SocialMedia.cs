using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class SocialMedia
{
    public Guid Id { get; set; }
    public SocialMediaName Name { get; set; }
    public string Url { get; set; } 
    public string? Icon { get; set; } 
    
    public User User { get; set; } 
}