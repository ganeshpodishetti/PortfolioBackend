using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Experience
{
    public string Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public ExperienceType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; } 
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    
    public User User { get; init; } = null!;
}