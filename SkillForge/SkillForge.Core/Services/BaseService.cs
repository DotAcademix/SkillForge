﻿using SkillForge.Core.Services.Abstraction;
using SkillForge.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkillForge.Core.Services;

public abstract class BaseService<TEntity, TPrototype>(IRepository<TEntity> repository) : IService<TEntity, TPrototype>
    where TEntity : class
    where TPrototype : class
{
    private readonly IRepository<TEntity> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => this._repository.GetAsync([e => e.Id == id, ..this.BuildAdditionalFilter()], cancellationToken);

    public Task<TEntity?> GetByIdCompleteAsync(Guid id, CancellationToken cancellationToken)
        => this._repository.GetCompleteAsync([e => e.Id == id, .. this.BuildAdditionalFilter()], cancellationToken);

    public Task<TEntity[]> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => this._repository.GetManyAsync([e => ids.Contains(e.Id), .. this.BuildAdditionalFilter()], cancellationToken);
    public Task<TEntity?> GetByIdWithNavigationsAsync(Guid id, IEnumerable<string> navigations, CancellationToken cancellationToken)
        => this._repository.GetWithNavigationsAsync([e => e.Id == id, .. this.BuildAdditionalFilter()], navigations, cancellationToken);
    public async Task<TEntity?> GetByIdRequiredAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity? entity = await this._repository.GetAsync([e => e.Id == id, .. this.BuildAdditionalFilter()], cancellationToken);
        if(entity is null) throw new InvalidOperationException("Entity was not found.");
        return entity;
    }


    //CRUD
    public async Task<TEntity> CreateAsync(TPrototype prototype, CancellationToken cancellationToken)
    {
        TEntity entity = await this.InitializeAsync(prototype, cancellationToken);

        await this.ApplyAsync(entity, prototype, cancellationToken);

        await this._repository.CreateAsync(entity, cancellationToken);
        return entity;
    }
    public async Task<TEntity> UpdateAsync(Guid entityId, TPrototype prototype, CancellationToken cancellationToken)
    {
        TEntity entity = await this.GetByIdRequiredAsync(entityId, cancellationToken);
        

        await this.ApplyAsync(entity, prototype, cancellationToken);

        await this._repository.UpdateAsync(entity, cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken)
    {
        TEntity entity = await this.GetByIdRequiredAsync(entityId, cancellationToken);

        await this._repository.DeleteAsync(entity, cancellationToken);
    }

    // Maybe will be added later
    //public async Task SoftDeleteAsync(Guid entityId, CancellationToken cancellationToken)
    //{
    //    TEntity entity = await this GetByIdRequiredAsync(entityId, cancellationToken);

    //    entity.IsDeleted = true;
    //    await this._repository.UpdateAsync(entity, cancellationToken);

    //}


    protected abstract Task<TEntity> InitializeAsync(TPrototype prototype, CancellationToken cancellationToken);
    protected abstract Task ApplyAsync(TEntity entity, TPrototype prototype, CancellationToken cancellationToken);

    protected virtual IEnumerable<Expression<Func<TEntity, bool>>> BuildAdditionalFilter() => [];
}

    // TODO: fix the id errors by adding base entity interface inside the .data project?
    // add base entity class?
    // error inside DeleteAsync