using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Mr
{
    public interface IMrGridKpiRepository : IRepository<MrGridKpi>, IMatchRepository<MrGridKpi, MrGridKpiDto>, ISaveChanges
    {
        
    }
}