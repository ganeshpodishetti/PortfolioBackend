using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Domain.Entities;

public sealed class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AboutMe { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    
    // Navigation properties
    public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
}