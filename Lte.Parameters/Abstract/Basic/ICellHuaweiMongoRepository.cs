using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Lte.Parameters.Abstract.Basic
{
    public interface ICellHuaweiMongoRepository : IRepository<CellHuaweiMongo, ObjectId>
    {
        List<CellHuaweiMongo> GetAllList(int eNodebId);

        List<CellHuaweiMongo> GetRecentList(int eNodebId);

        CellHuaweiMongo GetRecent(int eNodebId, byte sectorId);

        CellHuaweiMongo GetByLocal(int eNodebId, int localCellId);
    }
}
