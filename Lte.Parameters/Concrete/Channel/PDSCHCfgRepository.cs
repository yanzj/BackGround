using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class PDSCHCfgRepository : MongoDbRepositoryBase<PDSCHCfg, ObjectId>, IPDSCHCfgRepository
    {
        public PDSCHCfgRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PDSCHCfg";
        }

        public PDSCHCfgRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public PDSCHCfg GetRecent(int eNodebId, int localCellId)
        {
            return this.QueryRecent(eNodebId, localCellId);
        }
    }
}
