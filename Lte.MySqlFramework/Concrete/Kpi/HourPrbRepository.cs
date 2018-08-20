using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class HourPrbRepository : EfRepositorySave<MySqlContext, HourPrb>, IHourPrbRepository
    {
        public HourPrbRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public HourPrb Match(HourPrb stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }
    }
}
