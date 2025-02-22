using SkillForge.Data;
using SkillForge.Data.Entities;

namespace SkillForge.Models.Courses;

public class CoursesViewModel
{

    public  string Name { get; set; } = "";
    public  string Description { get; set; } = "";
    public  List<Module> Modules { get; set; } = new();
    public  List<ApplicationUser> EnrolledUsers { get; set; } = new();
    public  List<ApplicationUser> ManagerUsers { get; set; } = new();
}
