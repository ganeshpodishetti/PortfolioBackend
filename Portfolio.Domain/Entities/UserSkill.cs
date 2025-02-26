namespace Portfolio.Domain.Entities;

public class UserSkill
{
    public string Id { get; set; } 
    public string SkillId { get; set; }
    public string? Proficiency { get; set; }
    
    // Navigation properties
    public Skill Skill { get; set; }
    public User User { get; set; }
}