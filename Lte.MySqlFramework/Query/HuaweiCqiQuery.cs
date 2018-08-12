using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Query
{
    public class HuaweiCqiQuery : ZteDateSpanQuery<CqiHuawei, CqiView, ICqiHuaweiRepository>
    {
        public HuaweiCqiQuery(ICqiHuaweiRepository huaweiRepository, int eNodebId, byte sectorId) 
            : base(huaweiRepository, eNodebId, sectorId)
        {
        }

        protected override List<CqiHuawei> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId &&
                        x.SectorId == SectorId);
        }
    }
}