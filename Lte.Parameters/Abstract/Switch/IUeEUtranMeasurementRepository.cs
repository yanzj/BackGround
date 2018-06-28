using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IUeEUtranMeasurementRepository : IRepository<UeEUtranMeasurementZte, ObjectId>
    {
        UeEUtranMeasurementZte GetRecent(int eNodebId, int measIndex);
    }
}
