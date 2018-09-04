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
    public class TownHourCqiRepository : EfRepositorySave<MySqlContext, TownHourCqi>, ITownHourCqiRepository
    {
        public TownHourCqiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownHourCqi Match(TownHourCqi stat)
        {
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate == stat.StatDate);
        }
    }
}
