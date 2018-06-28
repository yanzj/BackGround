using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class ZteQciQuery : ZteDateSpanQuery<QciZte, QciView, IQciZteRepository>
    {
        public ZteQciQuery(IQciZteRepository zteRepository, int eNodebId, byte sectorId)
            : base(zteRepository, eNodebId, sectorId)
        {
        }

        protected override List<QciZte> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId && x.SectorId == SectorId);
        }
    }
}