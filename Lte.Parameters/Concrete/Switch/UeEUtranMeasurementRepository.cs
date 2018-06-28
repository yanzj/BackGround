using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;
using System.Linq;

namespace Lte.Parameters.Concrete.Switch
{
    public class UeEUtranMeasurementRepository : MongoDbRepositoryBase<UeEUtranMeasurementZte, ObjectId>, IUeEUtranMeasurementRepository
    {
        public UeEUtranMeasurementRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "UeEUtranMeasurement";
        }

        public UeEUtranMeasurementRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public UeEUtranMeasurementZte GetRecent(int eNodebId, int measIndex)
        {
            var query =
                MongoDB.Driver.Builders.Query<UeEUtranMeasurementZte>.Where(
                    e => e.eNodeB_Id == eNodebId && e.measCfgIdx == measIndex);
            var list = Collection.Find(query).AsQueryable();
            if (!list.Any()) return null;
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
