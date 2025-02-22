using AutoMapper;
using SkillForge.Data.Entities;
using SkillForge.Models.Courses;
using SkillForge.Core.Prototypes;

namespace SkillForge.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        this.CreateMap<CourseViewModel, Course>();
        this.CreateMap<CourseViewModel, CoursePrototype>();
        this.CreateMap<Course, CourseViewModel>();
        this.CreateMap<Course, CoursePrototype>();
        this.CreateMap<SkillForge.Data.Entities.Course, SkillForge.Components.Pages.Courses.Course>();
    }
}
