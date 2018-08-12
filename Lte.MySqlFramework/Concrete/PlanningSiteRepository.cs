using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class PlanningSiteRepository : EfRepositorySave<MySqlContext, PlanningSite>, IPlanningSiteRepository
    {
        public PlanningSiteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PlanningSite Match(PlanningSiteExcel stat)
        {
            return FirstOrDefault(x => x.PlanNum == stat.PlanNum);
        }
    }
}