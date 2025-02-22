using AutoMapper;
using SkillForge.Data.Entities;
using SkillForge.Models.Courses;
using SkillForge.Core.Prototypes;

namespace SkillForge.Profiles;

public class CoursesProfile : Profile
{
    public CoursesProfile()
    {
        this.CreateMap<CoursesViewModel, Course>();
        this.CreateMap<Course, CoursePrototype>();
        this.CreateMap<SkillForge.Data.Entities.Course, SkillForge.Components.Pages.Course>();
    }
}
