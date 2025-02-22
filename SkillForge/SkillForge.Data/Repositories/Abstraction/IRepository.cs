namespace SkillForge.Data.Repositories.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkillForge.Data.Entities;
using SkillForge.Data.Sorting;

public interface IRepository<TEntity>
    where TEntity : class, IEntity<string>
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken);

    Task<TProjection?> GetAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , Expression<Func<TEntity, TProjection>> projection, CancellationToken cancellationToken);

    Task<TEntity?> GetCompleteAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , CancellationToken cancellationToken);

    Task<TEntity?> GetWithNavigationsAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , IEnumerable<string> navigations, CancellationToken cancellationToken);

    Task<TEntity[]> GetManyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , CancellationToken cancellationToken);

    Task<TProjection[]> GetManyAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , Expression<Func<TEntity, TProjection>> projection, CancellationToken cancellationToken);

    Task<TProjection[]> GetManyAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters
        , Expression<Func<TEntity, TProjection>> projection, IEnumerable<IOrderClause<TEntity>> orderClauses
        , CancellationToken cancellationToken);
}