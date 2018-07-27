using Abp.EntityFramework.Channel;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class CellUlpcCommRepository : MongoDbRepositoryBase<CellUlpcComm, ObjectId>, ICellUlpcCommRepository
    {
        public CellUlpcCommRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellUlpcComm";
        }

        public CellUlpcCommRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public CellUlpcComm GetRecent(int eNodebId, int localCellId)
        {
            return this.QueryRecent(eNodebId, localCellId);
        }
    }
}