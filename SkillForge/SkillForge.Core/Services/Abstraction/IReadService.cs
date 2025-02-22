using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Data.Entities;

namespace SkillForge.Core.Services.Abstraction;

public interface IReadService<TEntity>
    where TEntity : class, IEntity<string>
{
    Task<TEntity[]> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdRequiredAsync(string id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdCompleteAsync(string id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdWithNavigationsAsync(string id, IEnumerable<string> navigations, CancellationToken cancellationToken);
}
