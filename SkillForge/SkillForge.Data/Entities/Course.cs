namespace SkillForge.Data.Entities;

using Microsoft.AspNetCore.Identity;
public class Course : Course<string>
{
    public Course()
    {
        Id = Guid.NewGuid().ToString();
    }
}

public class Course<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Module> Modules { get; set; } = new();
    public List<ApplicationUser> EnrolledUsers { get; set; } = new();
    public List<ApplicationUser> ManagerUsers { get; set; } = new();
}