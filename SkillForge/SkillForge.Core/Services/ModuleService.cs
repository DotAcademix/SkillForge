using SkillForge.Core.Authentication.Abstraction;
using SkillForge.Core.Authentication.Extentions;
using SkillForge.Core.Prototypes;
using SkillForge.Data.Entities;
using SkillForge.Data.Repositories.Abstraction;

namespace SkillForge.Core.Services;

public class ModuleService(IRepository<Module> repository, IAuthenticationContext authContext) : BaseService<Module, ModulePrototype>(repository)
{
    private readonly IAuthenticationContext _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
    protected override Task<Module> InitializeAsync(ModulePrototype prototype, CancellationToken cancellationToken)
        => Task.FromResult(new Module());

    protected override Task ApplyAsync(Module entity, ModulePrototype prototype, CancellationToken cancellationToken)
    {
        entity.Name = prototype.Name;
        entity.Content = prototype.Content;
        entity.VideoUrl = prototype.VideoUrl;
        entity.Course = prototype.Course;

        this._authContext.GetCurrentRequired();

        return Task.CompletedTask;
    }
}