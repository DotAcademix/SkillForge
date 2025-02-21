using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SkillForge.Core.Protorypes;
using SkillForge.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services.Abstraction;

public interface ICourseService : IService<Course, CoursesPrototype>
{

}
