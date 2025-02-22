using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SkillForge.Core.Prototypes;
using SkillForge.Data.Entities;

namespace SkillForge.Core.Services.Abstraction;

public interface IModuleService : IService<Module, ModulePrototype>
{

}
