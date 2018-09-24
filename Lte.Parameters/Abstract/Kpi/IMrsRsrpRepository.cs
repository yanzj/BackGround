using Abp.Domain.Repositories;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IMrsRsrpRepository : IRepository<MrsRsrpStat, ObjectId>, IStatDateCellRepository<MrsRsrpStat>
    {
        
    }
}