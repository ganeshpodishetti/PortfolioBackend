namespace Portfolio.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; } 
    public string Description { get; set; } 
    public string? Url { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    public User User { get; set; } 
}