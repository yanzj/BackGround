using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IDoubleFlowZteRepository : IRepository<DoubleFlowZte>, ISaveChanges,
        IMatchRepository<DoubleFlowZte>, IFilterTopRepository<DoubleFlowZte>
    {
    }
}