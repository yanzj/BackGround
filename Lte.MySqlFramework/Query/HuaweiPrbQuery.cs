using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Kpi;

namespace Lte.MySqlFramework.Query
{
    public class HuaweiPrbQuery : HuaweiDateSpanQuery<PrbHuawei, PrbView, IPrbHuaweiRepository>
    {
        public HuaweiPrbQuery(IPrbHuaweiRepository huaweiRepository, ICellRepository huaweiCellRepository, int eNodebId,
            byte sectorId) : base(huaweiRepository, huaweiCellRepository, eNodebId, sectorId)
        {
        }

        protected override List<PrbHuawei> QueryList(DateTime begin, DateTime end, byte localCellId)
        {
            return
                HuaweiRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId &&
                        x.LocalCellId == localCellId);
        }
    }
}
