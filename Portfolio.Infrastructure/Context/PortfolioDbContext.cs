using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Context;

internal class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) 
    : IdentityDbContext<User>(options)
{
       internal DbSet<Experience> Experiences { get; set; }
       internal DbSet<Skill> Skills { get; set; }
       internal DbSet<SocialMedia> SocialMedias { get; set; }
       internal DbSet<UserSkill> UserSkills { get; set; }
       internal DbSet<Education> Educations { get; set; }
       internal DbSet<Project> Projects { get; set; }
       
       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);
           
           // Remove AspNet prefix from tables
           foreach (var entityType in modelBuilder.Model.GetEntityTypes())
           {
               var tableName = entityType.GetTableName();
               if (tableName.StartsWith("AspNet"))
               {
                   entityType.SetTableName(tableName.Substring(6));
               }
           }
           
           // User - Primary Key
           modelBuilder.Entity<User>()
               .HasKey(u => u.Id);
           
           // Skill - Primary Key
           modelBuilder.Entity<Skill>()
               .HasKey(s => s.Id);
           
           // Many-to-Many: User ↔ Skill via User_Skill
           modelBuilder.Entity<UserSkill>()
               .HasKey(us => us.Id);
           
           modelBuilder.Entity<UserSkill>()
               .HasOne(us => us.User)
               .WithMany(u => u.Skills)
               .HasForeignKey(us => us.Id)
                //when a parent entity is deleted, all related child entities are automatically deleted as well.
               .OnDelete(DeleteBehavior.Cascade); 
           
           // Experience: One-to-Many with User
           modelBuilder.Entity<Experience>()
               .HasKey(e => e.Id);
           
           modelBuilder.Entity<Experience>()
               .HasOne(e => e.User)
               .WithMany(u => u.Experiences)
               .HasForeignKey(e => e.Id)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Project: One-to-Many with User
           modelBuilder.Entity<Project>()
               .HasKey(p => p.Id);

           modelBuilder.Entity<Project>()
               .HasOne(p => p.User)
               .WithMany(u => u.Projects)
               .HasForeignKey(p => p.Id)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Education: One-to-Many with User
           modelBuilder.Entity<Education>()
               .HasKey(ed => ed.Id);

           modelBuilder.Entity<Education>()
               .HasOne(ed => ed.User)
               .WithMany(u => u.Educations)
               .HasForeignKey(ed => ed.Id)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Social Media: One-to-Many with User
           modelBuilder.Entity<SocialMedia>()
               .HasKey(sm => sm.Id);

           modelBuilder.Entity<SocialMedia>()
               .HasOne(sm => sm.User)
               .WithMany(u => u.SocialMedias)
               .HasForeignKey(sm => sm.Id)
               .OnDelete(DeleteBehavior.Cascade);
       }
}