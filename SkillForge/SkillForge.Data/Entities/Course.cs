namespace SkillForge.Data.Entities;

public class Course
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Module> Modules { get; set; } = new();
    public List<ApplicationUser> EnrolledUsers { get; set; } = new();
    public List<ApplicationUser> ManagerUsers { get; set; } = new();

    public Course()
    {
        Id = Guid.NewGuid().ToString();
    }
}