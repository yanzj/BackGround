using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;
using MongoDB.Bson;

namespace Abp.EntityFramework.Dependency
{
    public static class HuaweiRepositoryQueries
    {
        public static TEntity QueryRecent<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId, int localCellId)
            where TEntity : class, IHuaweiCellMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }

        public static TEntity QueryRecent<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId)
            where TEntity : class, IHuaweiMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(e => e.eNodeB_Id == eNodebId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }

        public static List<TEntity> QueryRecentList<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId, int localCellId)
            where TEntity : class, IHuaweiCellMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public static List<TEntity> QueryReverseList<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int destENodebId, byte destSectorId)
            where TEntity : class, IHuaweiNeighborMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.Where(
                    e => e.eNodeBId == destENodebId && e.CellId == destSectorId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public static List<TEntity> QueryHuaweiRecentList<TEntity>(this MongoDbRepositoryBase<TEntity, ObjectId> repository,
            int eNodebId)
            where TEntity : class, IHuaweiMongo, IEntity<ObjectId>
        {
            var query =
                MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.eNodeB_Id, eNodebId);
            var list = repository.QueryCursor(query);
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}