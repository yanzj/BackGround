using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface IPlanningSiteRepository : IRepository<PlanningSite>,
        IMatchRepository<PlanningSite, PlanningSiteExcel>, ISaveChanges
    {
        
    }
}