﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using yummyApp.Application.Paging;
using yummyApp.Domain.Common;
using System.Collections;
using yummyApp.Application.Dynamic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace yummyApp.Application.Repositories
{
    public class EfRepositoryBase<TEntity, TKey, TContext> : IAsyncRepository<TEntity, TKey>, IRepository<TEntity, TKey>
     where TEntity : BaseAuditableEntity<TKey>
     where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Query() => Context.Set<TEntity>();
        

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.CreatedAt = DateTime.UtcNow;
            await Context.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.LastModifiedAt = DateTime.UtcNow;
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.LastModifiedAt = DateTime.UtcNow;
            Context.UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            var deletedEntity = await GetAsync(x => x.Id.ToString() == entity.Id.ToString());
            if (deletedEntity != null)
            {
                await setEntityAsSoftDeletedAsync(deletedEntity);
                await Context.SaveChangesAsync();
                return entity;
            }
            else
            {
                return entity;
            }
            
        }


        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entities, permanent);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<ICollection<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToListAsync(cancellationToken);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetPaginateListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, from: 0, cancellationToken);
            return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
        }

        public async Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListByDynamicAsync(
            DynamicQuery dynamic,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.AnyAsync(cancellationToken);
        }

        public TEntity Add(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            Context.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.CreatedAt = DateTime.UtcNow;
            Context.AddRange(entities);
            Context.SaveChanges();
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            entity.LastModifiedAt = DateTime.UtcNow;
            Context.Update(entity);
            Context.SaveChanges();
            return entity;
        }

        public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.LastModifiedAt = DateTime.UtcNow;
            Context.UpdateRange(entities);
            Context.SaveChanges();
            return entities;
        }

        public TEntity Delete(TEntity entity, bool permanent = false)
        {
            SetEntityAsDeleted(entity, permanent);
            Context.SaveChanges();
            return entity;
        }

        public ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false)
        {
            SetEntityAsDeleted(entities, permanent);
            Context.SaveChanges();
            return entities;
        }

        public TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true
        )
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return queryable.FirstOrDefault(predicate);
        }

        public IPaginate<TEntity> GetPaginateList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true
        )
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public ICollection<TEntity> GetList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false, bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToList();
            return queryable.ToList();
        }

        public IPaginate<TEntity> GetListByDynamic(
            DynamicQuery dynamic,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true
        )
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return queryable.ToPaginate(index, size);
        }

        public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return queryable.Any();
        }

        protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
        {
            if (!permanent)
            {
                if (entity.DeletedAt.HasValue)
                    return;

                entity.DeletedAt = DateTime.UtcNow;
                entity.LastModifiedAt = DateTime.UtcNow;
                //Context.Update(entity);
                await Context.SaveChangesAsync();
                //Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                Context.Remove(entity);
            }

            // İlişkili entity'leri işle
            foreach (var navigationEntry in Context.Entry(entity).Navigations)
            {
                if (navigationEntry is CollectionEntry collectionEntry && collectionEntry.CurrentValue != null)
                {
                    foreach (var dependentEntity in collectionEntry.CurrentValue)
                    {
                        await SetEntityAsDeletedAsync((TEntity)dependentEntity, permanent);
                    }
                }
                else if (navigationEntry is ReferenceEntry referenceEntry && referenceEntry.CurrentValue != null)
                {
                    await SetEntityAsDeletedAsync((TEntity)referenceEntry.CurrentValue, permanent);
                }
            }
        }




        protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
        {
            foreach (TEntity entity in entities)
                await SetEntityAsDeletedAsync(entity, permanent);
        }

        protected void SetEntityAsDeleted(TEntity entity, bool permanent)
        {
            if (!permanent)
            {
                CheckHasEntityHaveOneToOneRelation(entity);
                setEntityAsSoftDeleted(entity);
            }
            else
            {
                Context.Remove(entity);
            }
        }

        protected void SetEntityAsDeleted(IEnumerable<TEntity> entities, bool permanent)
        {
            foreach (TEntity entity in entities)
                SetEntityAsDeleted(entity, permanent);
        }

        protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
        {
            Type queryProviderType = query.Provider.GetType();
            MethodInfo createQueryMethod =
                queryProviderType
                    .GetMethods()
                    .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                    ?.MakeGenericMethod(navigationPropertyType)
                ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
            var queryProviderQuery =
                (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
            return queryProviderQuery.Where(x => !((TEntity)x).DeletedAt.HasValue);
        }

        protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
        {
            bool hasEntityHaveOneToOneRelation =
                Context
                    .Entry(entity)
                    .Metadata.GetForeignKeys()
                    .All(
                        x =>
                            x.DependentToPrincipal?.IsCollection == true
                            || x.PrincipalToDependent?.IsCollection == true
                            || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
                    ) == false;
            if (hasEntityHaveOneToOneRelation)
                throw new InvalidOperationException(
                    "Entity has one-to-one relationship. Soft Delete causes problems if you try to create entry again by same foreign key."
                );
        }

        private async Task setEntityAsSoftDeletedAsync(TEntity entity)
        {
            if (entity.DeletedAt.HasValue)
                return;
            entity.DeletedAt = DateTime.UtcNow;
            Context.Update(entity);

            var navigations = Context
                .Entry(entity)
                .Metadata.GetNavigations()
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();
            foreach (INavigation? navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned())
                    continue;
                if (navigation.PropertyInfo == null)
                    continue;

                object? navValue = navigation.PropertyInfo.GetValue(entity);
                if (navigation.IsCollection && navValue != null)
                {
                    foreach (TEntity navValueItem in (IEnumerable)navValue)
                        await setEntityAsSoftDeletedAsync(navValueItem);
                }
                else if (!navigation.IsCollection && navValue != null)
                {
                    await setEntityAsSoftDeletedAsync((TEntity)navValue);
                }
            }
        }

        private void setEntityAsSoftDeleted(TEntity entity)
        {
            if (entity.DeletedAt.HasValue)
                return;
            entity.DeletedAt = DateTime.UtcNow;

            var navigations = Context
                .Entry(entity)
                .Metadata.GetNavigations()
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();
            foreach (INavigation? navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned())
                    continue;
                if (navigation.PropertyInfo == null)
                    continue;

                object? navValue = navigation.PropertyInfo.GetValue(entity);
                if (navigation.IsCollection)
                {
                    if (navValue == null)
                    {
                        IQueryable query = Context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                        navValue = GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToList();
                        if (navValue == null)
                            continue;
                    }

                    foreach (TEntity navValueItem in (IEnumerable)navValue)
                        setEntityAsSoftDeleted(navValueItem);
                }
                else
                {
                    if (navValue == null)
                    {
                        IQueryable query = Context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                        navValue = GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).FirstOrDefault();
                        if (navValue == null)
                            continue;
                    }

                    setEntityAsSoftDeleted((TEntity)navValue);
                }
            }
            Context.Update(entity);
        }

        public async Task<string> SoftDelete(TKey id)
        {
            var entityItem = await GetAsync(x => x.Id.ToString()==id.ToString());

            if (entityItem == null) return "exist item";
            if (entityItem.DeletedAt.HasValue)
                return "Entity is deleted before" ;

            entityItem.DeletedAt = DateTime.UtcNow;

            Context.Update(entityItem);
           await Context.SaveChangesAsync();
            return entityItem.DeletedAt.ToString();
        }
    }
}
