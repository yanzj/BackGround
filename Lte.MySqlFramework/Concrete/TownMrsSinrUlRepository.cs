using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class TownMrsSinrUlRepository : EfRepositorySave<MySqlContext, TownMrsSinrUl>, ITownMrsSinrUlRepository
    {
        public TownMrsSinrUlRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownMrsSinrUl Match(TownMrsSinrUl stat)
        {
            var end = stat.StatDate.AddDays(1).Date;
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate >= stat.StatDate.Date && x.StatDate < end);
        }
    }
}
