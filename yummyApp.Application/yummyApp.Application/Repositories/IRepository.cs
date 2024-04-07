﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using yummyApp.Application.Dynamic;
using yummyApp.Application.Paging;
using yummyApp.Domain.Common;

namespace yummyApp.Application.Repositories
{
    public interface IRepository<TEntity, TKey> : IQuery<TEntity> where TEntity : IEntity<TKey>
    {
        TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true
        );

        IPaginate<TEntity> GetPaginateList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true
        );

        ICollection<TEntity> GetList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true
        );

        IPaginate<TEntity> GetListByDynamic(
            DynamicQuery dynamic,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true
        );

        bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true);
        TEntity Add(TEntity entity);
        ICollection<TEntity> AddRange(ICollection<TEntity> entities);
        TEntity Update(TEntity entity);
        ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);
        TEntity Delete(TEntity entity, bool permanent = false);
        ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);
    }

}
