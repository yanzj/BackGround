using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Query
{
    public class HuaweiRssiQuery : ZteDateSpanQuery<RssiHuawei, RssiView, IRssiHuaweiRepository>
    {
        public HuaweiRssiQuery(IRssiHuaweiRepository zteRepository, int eNodebId, byte sectorId) : base(
            zteRepository, eNodebId, sectorId)
        {
        }

        protected override List<RssiHuawei> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId &&
                        x.SectorId == SectorId);
        }
    }
}
