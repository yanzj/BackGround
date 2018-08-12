using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class DpiGridKpiRepository : EfRepositorySave<MySqlContext, DpiGridKpi>, IDpiGridKpiRepository
    {
        public DpiGridKpiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}