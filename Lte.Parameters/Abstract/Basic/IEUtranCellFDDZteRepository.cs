using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IEUtranCellFDDZteRepository : IRepository<EUtranCellFDDZte, ObjectId>
    {
        EUtranCellFDDZte GetRecent(int eNodebId, byte sectorId);
    }
}