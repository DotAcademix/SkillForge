using System.Security.Principal;

namespace SkillForge.MIddlewares;

public class AuthenticationContextMiddlewares(RequestDelegate next)
{
    private RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext httpContext)
    {
        IIdentity? identity = httpContext.User.Identity;

        if(identity is not null && httpContext.User.Identity.IsAuthenticated)
        {

        }

        await this._next(httpContext);
    }
}
