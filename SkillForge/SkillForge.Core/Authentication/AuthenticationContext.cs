using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SkillForge.Core.Authentication.Abstraction;
using SkillForge.Data;

namespace SkillForge.Core.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    public bool IsAuthenticated => this.CurrentUser != null;

    public ApplicationUser? CurrentUser { get; private set; }

    public void Authenticate(ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        this.CurrentUser = user;
    }
}
