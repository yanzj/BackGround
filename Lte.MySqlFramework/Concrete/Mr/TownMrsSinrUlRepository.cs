using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class TownMrsSinrUlRepository : EfRepositorySave<MySqlContext, TownMrsSinrUl>, ITownMrsSinrUlRepository
    {
        public TownMrsSinrUlRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownMrsSinrUl Match(TownMrsSinrUl stat)
        {
            var begin = stat.StatDate.Date;
            var end = stat.StatDate.AddDays(1).Date;
            return FirstOrDefault(x => x.TownId == stat.TownId && x.StatDate >= begin && x.StatDate < end
                                       && x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}
