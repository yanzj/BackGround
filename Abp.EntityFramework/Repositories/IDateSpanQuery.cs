using System;
using System.Collections.Generic;

namespace Abp.EntityFramework.Repositories
{
    public interface IDateSpanQuery<TEntity>
    {
        List<TEntity> GetAllList(DateTime begin, DateTime end);

        List<TEntity> GetAllList(int townId, DateTime begin, DateTime end);
    }
}