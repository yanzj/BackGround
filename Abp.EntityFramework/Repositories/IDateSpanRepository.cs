using System;
using System.Collections.Generic;

namespace Abp.EntityFramework.Repositories
{
    public interface IDateSpanRepository<TEntity>
    {
        List<TEntity> GetAllList(DateTime begin, DateTime end);

        List<TEntity> GetAllList(int townId, DateTime begin, DateTime end);
    }
}