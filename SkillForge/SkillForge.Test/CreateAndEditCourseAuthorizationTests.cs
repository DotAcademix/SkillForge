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

public class CreateAndEditCourseAuthorizationTests : TestContext
{
    private TestAuthorizationContext _authContext;
    public CreateAndEditCourseAuthorizationTests()
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
        Services.AddScoped<IModuleService, ModuleService>();
        Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        Services.AddDbContext<ApplicationDbContext>();
        
        _authContext = this.AddTestAuthorization();
        _authContext.SetAuthorized("testuser");
    }
    [Fact]
    public void AdminCanAccessCreateCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Admin);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Create>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<form", cut.Markup);
    }
    
    [Fact]
    public void InstructorCanNotAccessCreateCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Instructor);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Create>();
		

        // Assert
        Assert.Equal("", cut.Markup);
    }
    
    [Fact]
    public void StudentCanNotAccessCreateCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Student);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Create>();
		

        // Assert
        Assert.Equal("", cut.Markup);
    }
    
    [Fact]
    public void AdminCanAccessEditCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Admin);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Edit>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<form", cut.Markup);
    }
    
    [Fact]
    public void InstructorCanAccessEditCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Instructor);
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Edit>();
		

        // Assert
        Assert.NotEqual("", cut.Markup);

        Assert.Contains("<form", cut.Markup);
    }
    
    [Fact]
    public void StudentCanNotAccessEditCourse()
    {
        // Arrange
        _authContext.SetRoles(Role.Student);;
        
	
        // Act
        var cut = RenderComponent<SkillForge.Components.Pages.Courses.Edit>();
		

        // Assert
        Assert.Equal("", cut.Markup);
    }
}