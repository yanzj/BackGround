using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IPrbZteRepository : IRepository<PrbZte>, ISaveChanges, IMatchRepository<PrbZte>
    {
        
    }
}