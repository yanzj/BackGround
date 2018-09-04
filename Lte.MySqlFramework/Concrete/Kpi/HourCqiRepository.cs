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
    public class HourCqiRepository : EfRepositorySave<MySqlContext, HourCqi>, IHourCqiRepository
    {
        public HourCqiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public HourCqi Match(HourCqi stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }

        public List<HourCqi> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end &&
                     x.Cqi0Times + x.Cqi1Times + x.Cqi2Times + x.Cqi3Times + x.Cqi4Times > 30000);
        }
    }
}
