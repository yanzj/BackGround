using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Query
{
    public class HuaweiQciQuery : HuaweiDateSpanQuery<QciHuawei, QciView, IQciHuaweiRepository>
    {
        public HuaweiQciQuery(IQciHuaweiRepository huaweiRepository, ICellRepository huaweiCellRepository, int eNodebId,
            byte sectorId) : base(huaweiRepository, huaweiCellRepository, eNodebId, sectorId)
        {
        }

        protected override List<QciHuawei> QueryList(DateTime begin, DateTime end, byte localCellId)
        {
            return
                HuaweiRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId &&
                        x.LocalCellId == localCellId);
        }
    }
}