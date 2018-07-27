using Abp.Domain.Repositories;
using Abp.EntityFramework.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IPDSCHCfgRepository : IRepository<PDSCHCfg, ObjectId>
    {
        PDSCHCfg GetRecent(int eNodebId, int localCellId);
    }
}