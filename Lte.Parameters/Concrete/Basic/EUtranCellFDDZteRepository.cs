using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;
using System.Linq;

namespace Lte.Parameters.Concrete.Basic
{
    public class EUtranCellFDDZteRepository : MongoDbRepositoryBase<EUtranCellFDDZte, ObjectId>, IEUtranCellFDDZteRepository
    {
        public EUtranCellFDDZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EUtranCellFDD";
        }

        public EUtranCellFDDZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public EUtranCellFDDZte GetRecent(int eNodebId, byte sectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EUtranCellFDDZte>.Where(
                    e => e.eNodeB_Id == eNodebId && e.cellLocalId == sectorId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }

    public class PrachFDDZteRepository : MongoDbRepositoryBase<PrachFDDZte, ObjectId>, IPrachFDDZteRepository
    {
        public PrachFDDZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PrachFDD";
        }

        public PrachFDDZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public PrachFDDZte GetRecent(int eNodebId, byte sectorId)
        {
            return this.QueryRecent(eNodebId, sectorId);
        }
    }
}
