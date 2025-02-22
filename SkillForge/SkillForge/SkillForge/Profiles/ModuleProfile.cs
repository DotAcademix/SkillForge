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
        this.CreateMap<ModuleViewModel, SkillForge.Data.Entities.Module>();
        this.CreateMap<SkillForge.Data.Entities.Module, ModuleViewModel>();
        this.CreateMap<SkillForge.Data.Entities.Module, ModulePrototype>();
        this.CreateMap<SkillForge.Data.Entities.Module, SkillForge.Components.Pages.Module>();
    }
        
}
