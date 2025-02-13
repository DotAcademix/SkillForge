namespace SkillForge.Data.Entities;

using Microsoft.AspNetCore.Identity;
using SkillForge.Data.Enums;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public Role Role { get; set; }
}