namespace Portfolio.Domain.Entities;

public class UserSkill
{
    public string Id { get; set; } 
    public string SkillId { get; set; }
    public string? Proficiency { get; set; }
    
    // Navigation properties
    public Skill Skill { get; init; } = null!;
    public User User { get; init; } = null!;
}