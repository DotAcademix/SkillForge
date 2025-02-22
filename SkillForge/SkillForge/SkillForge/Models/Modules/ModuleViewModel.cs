using SkillForge.Data.Entities;

namespace SkillForge.Models.Modules
{
    public class ModuleViewModel
    {
        public string Name { get; set; } = "";
        public string Content { get; set; } = "";
        public string VideoUrl { get; set; } = "";
        public Course Course { get; set; }
    }
}
