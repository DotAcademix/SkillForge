using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Data;

namespace SkillForge.Core.Authentication.Abstraction;

public interface IAuthenticationContext
{
    [MemberNotNullWhen(true, nameof(CurrentUser))]
    //example: if (authContext.IsAuthenticated) { ... authContext.CurrentUser ... }
    bool IsAuthenticated { get; }

    ApplicationUser? CurrentUser { get; }

    void Authenticate(ApplicationUser user);
}
