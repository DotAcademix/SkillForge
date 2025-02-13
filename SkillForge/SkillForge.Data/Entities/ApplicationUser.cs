using Microsoft.AspNetCore.Identity;
using SkillForge.Data.Entities;
using SkillForge.Data.Enums;

namespace SkillForge.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Course> EnrolledCourses { get; set; } = new();
        public List<Course> ManagedCourses { get; set; } = new();
    }

}
