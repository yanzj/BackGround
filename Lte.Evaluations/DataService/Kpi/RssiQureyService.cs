using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.Kpi;
using Lte.MySqlFramework.Query;
using Lte.MySqlFramework.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService.Kpi
{
    public class RssiQueryService
        : FilterDateSpanQuery<RssiView, IRssiHuaweiRepository, IRssiZteRepository, RssiZte, RssiHuawei>
    {
        public RssiQueryService(IRssiHuaweiRepository huaweiRepository, IRssiZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected override IDateSpanQuery<List<RssiView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiRssiQuery(HuaweiRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<RssiView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteRssiQuery(ZteRepository, eNodebId, sectorId);
        }
    }
}
