using Microsoft.AspNetCore.Identity;

namespace Portfolio.Domain.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Password { get; set; } 
    public string? Country { get; set; } 
    public string? City { get; set; } 
    public string? AboutMe { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
}