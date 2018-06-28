using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class CellMeasGroupZteRepository : MongoDbRepositoryBase<CellMeasGroupZte, ObjectId>, ICellMeasGroupZteRepository
    {
        public CellMeasGroupZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellMeasGroup";
        }

        public CellMeasGroupZteRepository() : this(new MyMongoProvider("fangww"))
        {
            
        }

        public CellMeasGroupZte GetRecent(int eNodebId)
        {
            return this.QueryZteRecent(eNodebId);
        }
    }
}
