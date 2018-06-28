using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IPlanningSiteRepository : IRepository<PlanningSite>,
        IMatchRepository<PlanningSite, PlanningSiteExcel>, ISaveChanges
    {
        
    }
}