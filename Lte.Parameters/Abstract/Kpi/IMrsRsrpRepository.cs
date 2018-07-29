using Abp.Domain.Repositories;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IMrsRsrpRepository : IRepository<MrsRsrpStat, ObjectId>, IStatDateCellRepository<MrsRsrpStat>
    {
        
    }
}