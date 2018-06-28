using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class DoubleFlowZteRepository : EfRepositorySave<MySqlContext, DoubleFlowZte>, IDoubleFlowZteRepository
    {
        public DoubleFlowZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public DoubleFlowZte Match(DoubleFlowZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }
    }
}