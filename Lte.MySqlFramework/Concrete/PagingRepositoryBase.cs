using System;
using System.Linq;
using System.Linq.Expressions;
using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Microsoft.Practices.Unity.Utility;

namespace Lte.MySqlFramework.Concrete
{
    public abstract class PagingRepositoryBase<TEntity> : EfRepositorySave<MySqlContext, TEntity>, IPagingRepository<TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        public IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize,
            Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(predicate, "predicate");
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            return isAsc
                ? GetAll().Where(predicate)
                    .OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable()
                : GetAll().Where(predicate)
                    .OrderByDescending(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable();
        }

        public IQueryable<TEntity> GetAll<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            return isAsc
                ? GetAll().OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable()
                : GetAll().OrderByDescending(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable();
        }

        public PagingRepositoryBase(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}