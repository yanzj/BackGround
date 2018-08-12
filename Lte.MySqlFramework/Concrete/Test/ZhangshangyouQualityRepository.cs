using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.Test
{
    public class ZhangshangyouQualityRepository : EfRepositorySave<MySqlContext, ZhangshangyouQuality>,
        IZhangshangyouQualityRepository
    {
        public ZhangshangyouQualityRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public ZhangshangyouQuality Match(ZhangshangyouQualityCsv stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}
