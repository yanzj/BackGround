using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownHourUsersRepository : EfRepositorySave<MySqlContext, TownHourUsers>, ITownHourUsersRepository
    {
        public TownHourUsersRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownHourUsers Match(TownHourUsers stat)
        {
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate == stat.StatDate);
        }
    }
}
