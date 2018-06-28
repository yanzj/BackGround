using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;
using MongoDB.Bson;

namespace Abp.EntityFramework.Dependency
{
    public static class ZteRepositoryQueries
    {
        public static TEntity QueryRecent<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId, byte sectorId)
            where TEntity : class, IZteMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.eNodeB_Id == eNodebId && e.description == "cellLocalId=" + sectorId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }

        public static TEntity QueryZteRecent<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId)
            where TEntity : class, IZteMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.eNodeB_Id, eNodebId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }

        public static List<TEntity> QueryRecentList<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId)
            where TEntity : class, IZteMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(e => e.eNodeB_Id == eNodebId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public static List<TEntity> QueryZteRecentList<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId, byte sectorId)
            where TEntity : class, IZteMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.eNodeB_Id == eNodebId && e.description == "cellLocalId=" + sectorId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}