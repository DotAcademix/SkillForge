﻿using Microsoft.AspNetCore.Identity;
using SkillForge.Core.Authentication.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Authentication.Extentions;

public static class AuthenticationContextExtentions
{
    public static IdentityUser GetCurrentRequired(this IAuthenticationContext authContext)
    {
        if (!authContext.IsAuthenticated) throw new InvalidOperationException("This action requires an authinticated user.");

        return authContext.CurrentUser;
    }
}
