using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SkillForge.Core.Authentication.Abstraction;
using SkillForge.Core.Prototypes;
using SkillForge.Core.Services.Abstraction;
using SkillForge.Data.Entities;
using SkillForge.Data.Repositories.Abstraction;

namespace SkillForge.Core.Services;

public class CourseService(IRepository<Course> repository, IAuthenticationContext authContext) : BaseService<Course, CoursePrototype>(repository), ICourseService
{
    private readonly IAuthenticationContext _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
    protected override Task<Course> InitializeAsync(CoursePrototype prototype, CancellationToken cancellationToken)
        => Task.FromResult(new Course());

    protected override Task ApplyAsync(Course entity, CoursePrototype prototype, CancellationToken cancellationToken)
    {
        entity.Name = prototype.Name;
        entity.Description = prototype.Description;
        entity.Modules = prototype.Modules;
        entity.EnrolledUsers = prototype.EnrolledUsers;
        entity.ManagerUsers = prototype.ManagerUsers;


        return Task.CompletedTask;
    }

    /*protected override IEnumerable<Expression<Func<Course, bool>>> BuildAdditionalFilter()
    {
        IdentityUser currentUser = this._authContext.GetCurrentRequired();
        return [c => c.User.Id == currentUser.Id];
    }*/
}
//TODO: add the User and UserId inside the entity in .data project