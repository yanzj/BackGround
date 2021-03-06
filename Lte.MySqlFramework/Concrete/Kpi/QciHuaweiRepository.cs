using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.MySqlFramework.Concrete.Kpi
{
    public class QciHuaweiRepository : EfRepositorySave<MySqlContext, QciHuawei>, IQciHuaweiRepository
    {
        public QciHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public QciHuawei Match(QciHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }

        public List<QciHuawei> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end
                                                 && x.Cqi0Times + x.Cqi1Times + x.Cqi2Times + x.Cqi3Times + x.Cqi4Times
                                                 + x.Cqi5Times + x.Cqi6Times  > 3000000);
        }
    }
}