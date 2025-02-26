namespace Portfolio.Domain.Entities;

public class Project
{
    public string Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Url { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    
    public User User { get; init; } = null!;
}