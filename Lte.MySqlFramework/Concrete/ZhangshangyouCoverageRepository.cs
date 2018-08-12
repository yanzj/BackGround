using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
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
        
        public ZhangshangyouCoverage Match(ZhangshangyouCoverageCsv stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber
                                       && x.District == stat.District && x.Road == stat.Road);
        }
    }
}
