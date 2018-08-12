using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.MySqlFramework.Concrete.Test
{
    public class CoverageStatRepository : EfRepositorySave<MySqlContext, CoverageStat>, ICoverageStatRepository
    {
        public CoverageStatRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CoverageStat Match(CoverageStat stat)
        {
            return
                FirstOrDefault(
                    x => x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId && x.StatDate == stat.StatDate);
        }
    }
}