using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillForge.Data.Entities;

namespace SkillForge.Core.Services.Abstraction;

public interface ICreateService<TEntity, TPrototype>
    where TEntity : class, IEntity<string>
    where TPrototype : class
{
    Task<TEntity> CreateAsync(TPrototype prototype, CancellationToken cancellationToken);
}
