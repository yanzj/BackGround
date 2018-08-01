using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
{
    public class TownPreciseCoverage4GStatRepository : EfRepositorySave<EFParametersContext, TownPreciseCoverage4GStat>,
        ITownPreciseCoverage4GStatRepository
    {
        public TownPreciseCoverage4GStatRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownPreciseCoverage4GStat Match(TownPreciseCoverage4GStat stat)
        {
            var endTime = stat.StatTime.AddDays(1);
            return FirstOrDefault(
                x => x.TownId == stat.TownId && x.StatTime >= stat.StatTime && x.StatTime < endTime);
        }
    }
}