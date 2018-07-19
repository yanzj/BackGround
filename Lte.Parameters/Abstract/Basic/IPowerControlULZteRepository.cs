using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IPowerControlULZteRepository : IRepository<PowerControlULZte, ObjectId>
    {
        PowerControlULZte GetRecent(int eNodebId, byte sectorId);
    }
}