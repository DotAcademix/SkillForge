using AutoMapper;
using SkillForge.Data.Entities;
using SkillForge.Models.Courses;

namespace SkillForge.Profiles;

public class CoursesProfile : Profile
{
    public CoursesProfile()
    {
        this.CreateMap<Course, CoursesViewModel>();
    }
}
