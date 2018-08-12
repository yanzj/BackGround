using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class MrGridKpiRepository : EfRepositorySave<MySqlContext, MrGridKpi>, IMrGridKpiRepository
    {
        public MrGridKpiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public MrGridKpi Match(MrGridKpiDto stat)
        {
            return FirstOrDefault(x => x.X == stat.X && x.Y == stat.Y);
        }
    }
}