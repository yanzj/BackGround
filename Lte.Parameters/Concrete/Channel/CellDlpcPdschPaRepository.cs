using Abp.EntityFramework.Channel;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class CellDlpcPdschPaRepository : MongoDbRepositoryBase<CellDlpcPdschPa, ObjectId>, ICellDlpcPdschPaRepository
    {
        public CellDlpcPdschPaRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellDlpcPdschPa";
        }

        public CellDlpcPdschPaRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public CellDlpcPdschPa GetRecent(int eNodebId, int localCellId)
        {
            return this.QueryRecent(eNodebId, localCellId);
        }
    }
}
