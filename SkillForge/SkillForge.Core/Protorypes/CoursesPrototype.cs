using SkillForge.Data;
using SkillForge.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Protorypes;

public class CoursesPrototype
{

    
    public required string Name { get; init; }
    public required string Description { get; init; }

    // makes changes if needed
    public required List<Module> Modules { get; init; }
    public required List<ApplicationUser> EnrolledUsers { get; init; }
    public required List<ApplicationUser> ManagerUsers { get; init; }
}
