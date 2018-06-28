using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Channel;
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

    public interface IEUtranCellFDDZteRepository : IRepository<EUtranCellFDDZte, ObjectId>
    {
        EUtranCellFDDZte GetRecent(int eNodebId, byte sectorId);
    }

    public interface IPrachFDDZteRepository : IRepository<PrachFDDZte, ObjectId>
    {
        PrachFDDZte GetRecent(int eNodebId, byte sectorId);
    }

    public interface IPDSCHCfgRepository : IRepository<PDSCHCfg, ObjectId>
    {
        PDSCHCfg GetRecent(int eNodebId, int localCellId);
    }

    public interface ICellDlpcPdschPaRepository : IRepository<CellDlpcPdschPa, ObjectId>
    {
        CellDlpcPdschPa GetRecent(int eNodebId, int localCellId);
    }

    public interface ICellUlpcCommRepository : IRepository<CellUlpcComm, ObjectId>
    {
        CellUlpcComm GetRecent(int eNodebId, int localCellId);
    }

    public interface IPowerControlDLZteRepository : IRepository<PowerControlDLZte, ObjectId>
    {
        List<PowerControlDLZte> GetRecentList(int eNodebId);

        PowerControlDLZte GetRecent(int eNodebId, byte sectorId);
    }

    public interface IPowerControlULZteRepository : IRepository<PowerControlULZte, ObjectId>
    {
        PowerControlULZte GetRecent(int eNodebId, byte sectorId);
    }
}
