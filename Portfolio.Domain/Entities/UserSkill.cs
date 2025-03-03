namespace Portfolio.Domain.Entities;

public class UserSkill
{
    public Guid Id { get; set; } 
    public Guid SkillId { get; set; }
    public string? Proficiency { get; set; }
    
    // Navigation properties
    public Skill Skill { get; set; }
    public User User { get; set; }
}