using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface ICellUlpcCommRepository : IRepository<CellUlpcComm, ObjectId>
    {
        CellUlpcComm GetRecent(int eNodebId, int localCellId);
    }
}