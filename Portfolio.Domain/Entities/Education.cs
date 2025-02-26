namespace Portfolio.Domain.Entities;

public class Education
{
    public string Id { get; set; }
    public string SchoolName { get; set; } = null!;
    public string Degree { get; set; } = null!;
    public string FieldOfStudy { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } = null!;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    
    public User User { get; init; } = null!;
}