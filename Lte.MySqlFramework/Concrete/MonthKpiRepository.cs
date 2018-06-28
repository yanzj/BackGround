using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class MonthKpiRepository : EfRepositorySave<MySqlContext, MonthKpiStat>, IMonthKpiRepository
    {
        public MonthKpiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}