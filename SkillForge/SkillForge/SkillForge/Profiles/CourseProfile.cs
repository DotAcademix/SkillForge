using AutoMapper;
using SkillForge.Data.Entities;
using SkillForge.Models.Courses;
using SkillForge.Core.Prototypes;

namespace SkillForge.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        this.CreateMap<CoursesViewModel, Course>();
        this.CreateMap<Course, CoursesViewModel>();
        this.CreateMap<Course, CoursePrototype>();
        this.CreateMap<SkillForge.Data.Entities.Course, SkillForge.Components.Pages.Course>();
    }
}
