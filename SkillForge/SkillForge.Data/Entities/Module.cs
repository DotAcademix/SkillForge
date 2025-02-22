namespace SkillForge.Data.Entities;

public class Module : Module<string>
{
    public Module()
    {
        Id = Guid.NewGuid().ToString();
    }
}

public class Module<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string VideoUrl { get; set; }
    public Course Course { get; set; }
    //public ApplicationUser User { get; set; }
}