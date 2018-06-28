using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class EUtranCellMeasurementZteRepository : MongoDbRepositoryBase<EUtranCellMeasurementZte, ObjectId>, IEUtranCellMeasurementZteRepository
    {
        public EUtranCellMeasurementZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EUtranCellMeasurement";
        }

        public EUtranCellMeasurementZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EUtranCellMeasurementZte> GetRecentList(int eNodebId, byte sectorId)
        {
            return this.QueryZteRecentList(eNodebId, sectorId);
        }
    }
}
