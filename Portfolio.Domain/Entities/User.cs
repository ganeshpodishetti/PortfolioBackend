using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; }
    public string? Country { get; set; } 
    public string? City { get; set; } 
    public string? AboutMe { get; set; } 
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
}