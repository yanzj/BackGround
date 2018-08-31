using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities.Kpi;

namespace Lte.MySqlFramework.Query
{
    public class HourPrbDateSpanQuery : ZteDateSpanQuery<HourPrb, HourPrbView, IHourPrbRepository>
    {
        public HourPrbDateSpanQuery(IHourPrbRepository zteRepository, int eNodebId, byte sectorId) : base(zteRepository,
            eNodebId, sectorId)
        {
        }

        protected override List<HourPrb> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId && x.SectorId == SectorId);
        }
    }
}
