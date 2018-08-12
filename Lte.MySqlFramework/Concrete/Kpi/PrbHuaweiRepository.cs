using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class PrbHuaweiRepository : EfRepositorySave<MySqlContext, PrbHuawei>, IPrbHuaweiRepository
    {
        public PrbHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PrbHuawei Match(PrbHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }
    }
}