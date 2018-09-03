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
    public class HourUsersRepository : EfRepositorySave<MySqlContext, HourUsers>, IHourUsersRepository
    {
        public HourUsersRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public HourUsers Match(HourUsers stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }

        public List<HourUsers> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }
    }
}
