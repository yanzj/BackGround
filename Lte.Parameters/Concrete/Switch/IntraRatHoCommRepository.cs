using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class IntraRatHoCommRepository : MongoDbRepositoryBase<IntraRatHoComm, ObjectId>, IIntraRatHoCommRepository
    {
        public IntraRatHoCommRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "IntraRatHoComm";
        }

        public IntraRatHoCommRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public IntraRatHoComm GetRecent(int eNodebId)
        {
            return this.QueryRecent(eNodebId);
        }
    }
}
