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

public class CourseService(IRepository<Course> repository, IRepository<Module> moduleRepository, IAuthenticationContext authContext) : BaseService<Course, CoursePrototype>(repository), ICourseService
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
    
    public override async Task<Course?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        // Retrieve the course
        var course = await repository.GetAsync([c => c.Id == id], cancellationToken);
        if (course is null)
            return null;

        // Retrieve modules associated with the course
        var modules = await moduleRepository.GetManyAsync([m => m.Course.Id == id], cancellationToken);
        course.Modules = modules.ToList(); // Assign retrieved modules

        return course;
    }
    /*protected override IEnumerable<Expression<Func<Course, bool>>> BuildAdditionalFilter()
    {
        IdentityUser currentUser = this._authContext.GetCurrentRequired();
        return [c => c.User.Id == currentUser.Id];
    }*/
}