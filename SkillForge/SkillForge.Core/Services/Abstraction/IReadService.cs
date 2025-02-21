using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services.Abstraction;

public interface IReadService<TEntity>
    where TEntity : class
{
    Task<TEntity[]> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdRequiredAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdCompleteAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdWithNavigationsAsync(Guid id, IEnumerable<string> navigations, CancellationToken cancellationToken);
}
