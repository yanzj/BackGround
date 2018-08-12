using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Query
{
    public class ZteCqiQuery : ZteDateSpanQuery<CqiZte, CqiView, ICqiZteRepository>
    {
        public ZteCqiQuery(ICqiZteRepository zteRepository, int eNodebId, byte sectorId)
            : base(zteRepository, eNodebId, sectorId)
        {
        }

        protected override List<CqiZte> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId && x.SectorId == SectorId);
        }
    }
}