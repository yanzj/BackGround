using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownHourPrbRepository : EfRepositorySave<MySqlContext, TownHourPrb>, ITownHourPrbRepository
    {
        public TownHourPrbRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownHourPrb Match(TownHourPrb stat)
        {
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate == stat.StatDate);
        }
    }
}
