using SkillForge.Data.Enums;

namespace SkillForge.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillForge.Data.Entities;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Module> Modules { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUserRole>()
                .Property(r => r.Role)
                .HasConversion(
                    v => v.ToString(),  // Convert the enum to string when saving
                    v => (Role)Enum.Parse(typeof(Role), v)  // Convert string back to enum when reading
                );
            
            builder.Entity<Course>().ToTable("Courses", "dbo");
            builder.Entity<Module>().ToTable("Modules", "dbo");
            
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.EnrolledCourses)
                .WithMany(c => c.EnrolledUsers)
                .UsingEntity(j => j.ToTable("UserEnrolledCourses"));
            
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.ManagedCourses)
                .WithMany(c => c.ManagerUsers)
                .UsingEntity(j => j.ToTable("UserManagedCourses"));
            
            builder.Entity<Module>()
                .HasOne(m => m.Course)    // Each Module has one Course
                .WithMany(c => c.Modules) // Each Course has many Modules
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete if course is deleted
    }
}