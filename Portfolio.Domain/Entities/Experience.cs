using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Experience
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } 
    public string Position { get; set; } 
    public ExperienceType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; } 
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    public User User { get; set; } 
}