using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Abp.EntityFramework.Dependency
{
    public interface IStatDateCellRepository<out TEntity>
        where TEntity : class, IStatDateCell, IEntity<ObjectId>
    {
        TEntity Get(string cellId, DateTime statDate);

        IEnumerable<TEntity> GetList(string cellId, DateTime begin, DateTime end);
    }
}