using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HuaweiFlowQuery : HuaweiDateSpanQuery<FlowHuawei, FlowView, IFlowHuaweiRepository>
    {
        public HuaweiFlowQuery(IFlowHuaweiRepository huaweiRepository, ICellRepository huaweiCellRepository,
            int eNodebId, byte sectorId) 
            : base(huaweiRepository, huaweiCellRepository, eNodebId, sectorId)
        {
        }

        protected override List<FlowHuawei> QueryList(DateTime begin, DateTime end, byte localCellId)
        {
            return HuaweiRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end
                                                    && x.ENodebId == ENodebId
                                                    && x.LocalCellId == localCellId);
        }
    }
}