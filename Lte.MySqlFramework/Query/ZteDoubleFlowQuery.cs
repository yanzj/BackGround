using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Query
{
    public class ZteDoubleFlowQuery : ZteDateSpanQuery<DoubleFlowZte, DoubleFlowView, IDoubleFlowZteRepository>
    {
        public ZteDoubleFlowQuery(IDoubleFlowZteRepository zteRepository, int eNodebId, byte sectorId) : base(
            zteRepository, eNodebId, sectorId)
        {
        }

        protected override List<DoubleFlowZte> QueryList(DateTime begin, DateTime end)
        {
            return
                ZteRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == ENodebId && x.SectorId == SectorId);
        }
    }
}
