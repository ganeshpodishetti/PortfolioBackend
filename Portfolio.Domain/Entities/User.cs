using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Domain.Entities;

public class User : IdentityUser
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
    public virtual ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
}