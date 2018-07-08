using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
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