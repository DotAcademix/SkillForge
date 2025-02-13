namespace SkillForge.Data.Entities;

public class Module
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string VideoUrl { get; set; }
    public Course Course { get; set; }

    public Module()
    {
        Id = Guid.NewGuid().ToString();
    }
}