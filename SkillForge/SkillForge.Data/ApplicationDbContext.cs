using Microsoft.AspNetCore.Identity;
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

        var roles = new List<(string, string)> { ("Student", "0"), ("Instructor", "1"), ("Admin", "2") };

        foreach (var (roleName, roleId) in roles)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId, 
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
        }

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
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .OnDelete(DeleteBehavior.Cascade);
    }
}