namespace SkillForge.Core.Prototypes;

using SkillForge.Data.Entities;

public class ModulePrototype
{
    public string Name { get; init; }
    public string Content { get; init; }
    public string VideoUrl { get; init; }
    public Course Course { get; init; }
}