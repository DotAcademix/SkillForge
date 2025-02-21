using SkillForge.Data;
using SkillForge.Data.Entities;

namespace SkillForge.Models.Courses;

public class CoursesViewModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required List<Module> Modules { get; init; } = new();
    public required List<ApplicationUser> EnrolledUsers { get; init; } = new();
    public required List<ApplicationUser> ManagerUsers { get; init; } = new();
}
