namespace Portfolio.Domain.Entities;

public class Education
{
    public Guid Id { get; set; }
    public string SchoolName { get; set; } 
    public string Degree { get; set; } 
    public string FieldOfStudy { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } 
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    public User User { get; set; } 
}