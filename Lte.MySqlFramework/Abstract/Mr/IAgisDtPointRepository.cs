using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Mr
{
    public interface IAgisDtPointRepository : IRepository<AgisDtPoint>, ISaveChanges
    {
        
    }
}