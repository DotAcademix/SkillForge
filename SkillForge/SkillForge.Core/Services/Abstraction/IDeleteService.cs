using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services.Abstraction;

public interface IDeleteService
{
    // maybe add later - Task SoftDeleteAsync(Guid entityId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken);
}
