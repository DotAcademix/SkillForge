using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform;
using Moq;
using MudBlazor.Services;
using SkillForge.Core.Authentication;
using SkillForge.Core.Authentication.Abstraction;
using SkillForge.Core.Services;
using SkillForge.Core.Services.Abstraction;
using SkillForge.Data;
using SkillForge.Data.Entities;
using SkillForge.Data.Enums;
using SkillForge.Data.Repositories;
using SkillForge.Data.Repositories.Abstraction;
using SkillForge.Profiles;

namespace SkillForge.Test;

public class CreateAndEditModuleAuthorizationTests : TestContext
{
    private TestAuthorizationContext _authContext;
    
    public CreateAndEditModuleAuthorizationTests()
    {
        Services.AddMudServices();
        Assembly currentAssembly = Assembly.GetExecutingAssembly();
        Services.AddAutoMapper(currentAssembly);
        Services.AddAutoMapper(typeof(CourseProfile));
        Services.AddAutoMapper(typeof(ModuleProfile));
        Services.AddScoped<IAuthenticationContext, AuthenticationContext>();
        Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        var mockCourseService = new Mock<ICourseService>();
        mockCourseService
            .Setup(service => service.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Course?)new()); // Simulate "course not found"
        Services.AddScoped(_ => mockCourseService.Object);
        
        var mockModuleService = new Mock<IModuleService>();
        mockModuleService
            .Setup(service => service.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((SkillForge.Data.Entities.Module?)new()); // Simulate "module not found"
        Services.AddScoped(_ => mockModuleService.Object);
        
        Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        Services.AddDbContext<ApplicationDbContext>();
        
        _authContext = this.AddTestAuthorization();
        _authContext.SetAuthorized("testuser");
    }
    [Fact]
    public void AdminCanAccessCreateModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Admin);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Create>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<h3", cut.Markup);
    }
    
    [Fact]
    public void InstructorCanAccessCreateModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Instructor);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Create>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<h3", cut.Markup);
    }
    
    [Fact]
    public void StudentCanNotAccessCreateModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Student);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Create>();
		

        // Assert
        Assert.Equal("", cut.Markup);
    }
    
    [Fact]
    public void AdminCanAccessEditModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Admin);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Edit>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<h3", cut.Markup);
    }
    
    [Fact]
    public void InstructorCanAccessEditModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Instructor);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Edit>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<h3", cut.Markup);
    }
    
    [Fact]
    public void StudentCanNotAccessEditModule()
    {
        // Arrange
        _authContext.SetRoles(Role.Student);;
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Modules.Edit>();
		

        // Assert
        Assert.Equal("", cut.Markup);
    }
}