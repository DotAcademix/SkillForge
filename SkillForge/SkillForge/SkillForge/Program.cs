using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SkillForge.Client.Pages;
using SkillForge.Components;
using SkillForge.Components.Account;
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

namespace SkillForge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddAuthenticationStateSerialization();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            
            builder.Services.AddMudServices();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
            
            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            builder.Services.AddAutoMapper(currentAssembly);
            builder.Services.AddAutoMapper(typeof(CourseProfile));
            builder.Services.AddAutoMapper(typeof(ModuleProfile));
            //add dependencies

            builder.Services.AddScoped<IAuthenticationContext, AuthenticationContext>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IModuleService, ModuleService>();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.MapStaticAssets();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();
            
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
