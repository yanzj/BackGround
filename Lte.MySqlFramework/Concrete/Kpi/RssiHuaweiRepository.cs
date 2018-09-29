using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class RssiHuaweiRepository : EfRepositorySave<MySqlContext, RssiHuawei>, IRssiHuaweiRepository
    {
        public RssiHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public RssiHuawei Match(RssiHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

    }
}
