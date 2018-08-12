using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownQciRepository : EfRepositorySave<MySqlContext, TownQciStat>, ITownQciRepository
    {
        public TownQciRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}