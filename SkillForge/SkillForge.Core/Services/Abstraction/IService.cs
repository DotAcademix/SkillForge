using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services.Abstraction;

public interface IService<TEntity, TPrototype> : ICreateService<TEntity, TPrototype>, IReadService<TEntity>, IUpdateService<TEntity, TPrototype>, IDeleteService
    where TEntity : class
    where TPrototype : class
{

}
