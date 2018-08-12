using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Evaluations.DataService.Switch;
using Lte.Evaluations.Query;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class RrcQueryService : DateSpanQuery<RrcView, IRrcHuaweiRepository, IRrcZteRepository>
    {
        public RrcQueryService(IRrcHuaweiRepository huaweiRepository, IRrcZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected override Switch.IDateSpanQuery<List<RrcView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiRrcQuery(HuaweiRepository, HuaweiCellRepository, eNodebId, sectorId);
        }

        protected override Switch.IDateSpanQuery<List<RrcView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteRrcQuery(ZteRepository, eNodebId, sectorId);
        }

        public IEnumerable<RrcView> QueryTopRrcFailViews(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = HuaweiCellRepository.QueryDistrictFlowViews<RrcView, RrcZte, RrcHuawei>(city, district,
                ZteRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end &&
                        x.MoDataRrcRequest + x.MoSignallingRrcRequest + x.MtAccessRrcRequest > 20000),
                HuaweiRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end &&
                        x.MoDataRrcRequest + x.MoSignallingRrcRequest + x.MtAccessRrcRequest > 20000),
                TownRepository, ENodebRepository).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.TotalRrcFail).Take(topCount * days);
        }
    }
}