using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Kpi;
using Lte.MySqlFramework.Query;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class RrcQueryService : DateSpanQuery<RrcView, IRrcHuaweiRepository, IRrcZteRepository>
    {
        public RrcQueryService(IRrcHuaweiRepository huaweiRepository, IRrcZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected override IDateSpanQuery<List<RrcView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiRrcQuery(HuaweiRepository, HuaweiCellRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<RrcView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteRrcQuery(ZteRepository, eNodebId, sectorId);
        }

        public IEnumerable<RrcView> QueryTopRrcFailViews(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = HuaweiCellRepository.QueryDistrictFlowViews<RrcView, RrcZte, RrcHuawei>(city, district,
                ZteRepository.FilterTopList(begin, end),
                HuaweiRepository.FilterTopList(begin, end),
                TownRepository, ENodebRepository).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.TotalRrcFail).Take(topCount * days);
        }
    }
}