using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class PowerControlDLZteRepository : MongoDbRepositoryBase<PowerControlDLZte, ObjectId>, IPowerControlDLZteRepository
    {
        public PowerControlDLZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PowerControlDL";
        }

        public PowerControlDLZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<PowerControlDLZte> GetRecentList(int eNodebId)
        {
            return this.QueryRecentList(eNodebId);
        }

        public PowerControlDLZte GetRecent(int eNodebId, byte sectorId)
        {
            return this.QueryRecent(eNodebId, sectorId);
        }
    }
}