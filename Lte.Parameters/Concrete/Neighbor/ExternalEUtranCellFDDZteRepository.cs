using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class ExternalEUtranCellFDDZteRepository : MongoDbRepositoryBase<ExternalEUtranCellFDDZte, ObjectId>, IExternalEUtranCellFDDZteRepository
    {
        public ExternalEUtranCellFDDZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "ExternalEUtranCellFDD";
        }

        public ExternalEUtranCellFDDZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<ExternalEUtranCellFDDZte> GetRecentList(int eNodebId)
        {
            return this.QueryRecentList(eNodebId);
        }

        public List<ExternalEUtranCellFDDZte> GetReverseList(int destENodebId, byte destSectorId)
        {
            var query =
                   MongoDB.Driver.Builders.Query<ExternalEUtranCellFDDZte>.Where(
                       e => e.eNBId == destENodebId && e.cellLocalId == destSectorId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}
