using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class TownMrsTadvRepository : EfRepositorySave<MySqlContext, TownMrsTadv>, ITownMrsTadvRepository
    {
        public TownMrsTadvRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownMrsTadv Match(TownMrsTadv stat)
        {
            var begin = stat.StatDate.Date;
            var end = stat.StatDate.AddDays(1).Date;
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate >= begin && x.StatDate < end
                                       && x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}
