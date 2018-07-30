using Abp.Domain.Repositories;
using Abp.EntityFramework.Dependency;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IMrsSinrUlRepository : IRepository<MrsSinrUlStat, ObjectId>, IStatDateCellRepository<MrsSinrUlStat>
    {
        
    }
}