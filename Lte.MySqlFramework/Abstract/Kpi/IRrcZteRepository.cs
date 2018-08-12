using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IRrcZteRepository : IRepository<RrcZte>, ISaveChanges, IMatchRepository<RrcZte>,
        IFilterTopRepository<RrcZte>
    {
        
    }
}