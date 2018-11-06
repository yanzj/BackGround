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
    public class TopMrsTadvRepository : EfRepositorySave<MySqlContext, TopMrsTadv>, ITopMrsTadvRepository
    {
        public TopMrsTadvRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TopMrsTadv Match(TopMrsTadv stat)
        {
            var nextDate = stat.StatDate.Date.AddDays(1);
            return FirstOrDefault(
                x =>
                    x.StatDate >= stat.StatDate.Date && x.StatDate < nextDate && x.ENodebId == stat.ENodebId &&
                    x.SectorId == stat.SectorId);
        }
    }
}
