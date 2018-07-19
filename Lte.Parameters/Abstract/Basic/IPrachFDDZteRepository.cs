using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IPrachFDDZteRepository : IRepository<PrachFDDZte, ObjectId>
    {
        PrachFDDZte GetRecent(int eNodebId, byte sectorId);
    }
}