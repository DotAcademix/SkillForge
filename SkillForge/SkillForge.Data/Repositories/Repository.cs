﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkillForge.Data.Entities;
using SkillForge.Data.Repositories.Abstraction;
using SkillForge.Data.Repositories.Extentions;
using SkillForge.Data.Sorting;

namespace SkillForge.Data.Repositories;

public class Repository<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity>
    where TEntity : class, IEntity<string>
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        this._dbContext.Set<TEntity>().Add(entity);
        await this._dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        this._dbContext.Set<TEntity>().Update(entity);
        await this._dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        this._dbContext.Set<TEntity>().Remove(entity);
        await this._dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<TEntity?> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
        => this._dbContext.Set<TEntity>().Where(filters).FirstOrDefaultAsync(cancellationToken);


    public Task<TProjection?> GetAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TProjection>> projection, CancellationToken cancellationToken)
        => this._dbContext.Set<TEntity>().Where(filters).Select(projection).FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> GetCompleteAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        IEntityType? entityType = this._dbContext.Model.FindEntityType(typeof(TEntity));
        if (entityType is null) throw new InvalidOperationException("Invalid entity type.");

        IEnumerable<string> navigations = entityType.GetNavigations().Select(x => x.Name)
            .Concat(entityType.GetSkipNavigations().Select(x => x.Name));

        return this.GetWithNavigationsAsync(filters, navigations, cancellationToken);
    }
    public Task<TEntity?> GetWithNavigationsAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, IEnumerable<string> navigations, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = this._dbContext.Set<TEntity>().Where(filters);

        foreach (string navigation in navigations)
            query = query.Include(navigation);

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity[]> GetManyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
        => this._dbContext.Set<TEntity>().Where(filters).ToArrayAsync(cancellationToken);

    public Task<TProjection[]> GetManyAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TProjection>> projection, CancellationToken cancellationToken)
        => this._dbContext.Set<TEntity>().Where(filters).Select(projection).ToArrayAsync(cancellationToken);

    public Task<TProjection[]> GetManyAsync<TProjection>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TProjection>> projection, IEnumerable<IOrderClause<TEntity>> orderClauses, CancellationToken cancellationToken)
        => this._dbContext.Set<TEntity>().Where(filters).OrderBy(orderClauses).Select(projection).ToArrayAsync(cancellationToken);



}
