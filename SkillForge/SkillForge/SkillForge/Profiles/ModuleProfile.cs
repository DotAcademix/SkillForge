using AutoMapper;
using SkillForge.Components.Pages;
using SkillForge.Core.Prototypes;
using SkillForge.Data.Entities;
using SkillForge.Models.Modules;

namespace SkillForge.Profiles;

public class ModuleProfile : Profile
{
    public ModuleProfile()
    {
        this.CreateMap<ModuleViewModel, Module>();
        this.CreateMap<Module, ModuleViewModel>();
        this.CreateMap<Module, ModulePrototype>();
       //this.CreateMap<SkillForge.Data.Entities.Course, SkillForge.Components.Pages.Moduke>();
    }
        
}
