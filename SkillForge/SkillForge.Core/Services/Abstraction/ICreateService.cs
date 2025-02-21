using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services.Abstraction;

public interface ICreateService<TEntity, TPrototype>
    where TEntity : class
    where TPrototype : class
{
    Task<TEntity> CreateAsync(TPrototype prototype, CancellationToken cancellationToken);
}
