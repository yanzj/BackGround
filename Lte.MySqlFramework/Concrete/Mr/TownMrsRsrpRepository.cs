using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class TownMrsRsrpRepository : EfRepositorySave<MySqlContext, TownMrsRsrp>, ITownMrsRsrpRepository
    {
        public TownMrsRsrpRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownMrsRsrp Match(TownMrsRsrp stat)
        {
            var beginTime = stat.StatDate.Date;
            var endTime = stat.StatDate.Date.AddDays(1);
            return FirstOrDefault(
                x => x.TownId == stat.TownId && x.StatDate >= beginTime && x.StatDate < endTime &&
                     x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}