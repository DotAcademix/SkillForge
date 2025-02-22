using System.Reflection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SkillForge.Core.Authentication;
using SkillForge.Core.Authentication.Abstraction;
using SkillForge.Core.Services;
using SkillForge.Core.Services.Abstraction;
using SkillForge.Data;
using SkillForge.Data.Repositories;
using SkillForge.Data.Repositories.Abstraction;

namespace SkillForge.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthenticationStateDeserialization();
            
            await builder.Build().RunAsync();
        }
    }
}
