using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Repositories
{
    public class EfRepositorySave<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>, ISaveChanges
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfRepositorySave(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}