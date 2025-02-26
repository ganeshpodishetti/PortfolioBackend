using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Skill
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public SkillType Type { get; set; }
    
    // Navigation properties
    public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}