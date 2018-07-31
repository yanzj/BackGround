using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class TopMrsSinrUlRepository : EfRepositorySave<MySqlContext, TopMrsSinrUl>, ITopMrsSinrUlRepository
    {
        public TopMrsSinrUlRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TopMrsSinrUl Match(TopMrsSinrUl stat)
        {
            var nextDate = stat.StatDate.Date.AddDays(1);
            return FirstOrDefault(
                x =>
                    x.StatDate >= stat.StatDate.Date && x.StatDate < nextDate && x.ENodebId == stat.ENodebId &&
                    x.SectorId == stat.SectorId);
        }
    }
}
