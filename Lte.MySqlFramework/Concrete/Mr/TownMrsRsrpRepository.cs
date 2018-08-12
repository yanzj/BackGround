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
            var endTime = stat.StatDate.AddDays(1);
            return FirstOrDefault(
                x => x.TownId == stat.TownId && x.StatDate >= stat.StatDate && x.StatDate < endTime);
        }
    }
}