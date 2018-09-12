using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface ICellDlpcPdschPaRepository : IRepository<CellDlpcPdschPa, ObjectId>
    {
        CellDlpcPdschPa GetRecent(int eNodebId, int localCellId);
    }
}