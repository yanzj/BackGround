using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class ZhangshangyouCoverageRepository : EfRepositorySave<MySqlContext, ZhangshangyouCoverage>,
        IZhangshangyouCoverageRepository
    {
        public ZhangshangyouCoverageRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public ZhangshangyouCoverage Match(ZhangshangyouCoverage stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}
