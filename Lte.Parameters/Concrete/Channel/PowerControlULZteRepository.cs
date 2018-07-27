using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class PowerControlULZteRepository : MongoDbRepositoryBase<PowerControlULZte, ObjectId>,
        IPowerControlULZteRepository
    {
        public PowerControlULZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PowerControlUL";
        }

        public PowerControlULZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public PowerControlULZte GetRecent(int eNodebId, byte sectorId)
        {
            return this.QueryRecent(eNodebId, sectorId);
        }
    }
}