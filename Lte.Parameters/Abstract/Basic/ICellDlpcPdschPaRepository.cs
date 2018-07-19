using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface ICellDlpcPdschPaRepository : IRepository<CellDlpcPdschPa, ObjectId>
    {
        CellDlpcPdschPa GetRecent(int eNodebId, int localCellId);
    }
}