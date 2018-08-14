using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Query;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class DoubleFlowQueryService
        : FilterDateSpanQuery<DoubleFlowView, IDoubleFlowHuaweiRepository, IDoubleFlowZteRepository, DoubleFlowZte,
            DoubleFlowHuawei>
    {
        private readonly ICellRepository _cellRepository;

        public DoubleFlowQueryService(IDoubleFlowHuaweiRepository huaweiRepository,
            IDoubleFlowZteRepository zteRepository, IENodebRepository eNodebRepository,
            ICellRepository huaweiCellRepository, ITownRepository townRepository, ICellRepository cellRepository) :
            base(huaweiRepository,
                zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
            _cellRepository = cellRepository;
        }

        protected override IDateSpanQuery<List<DoubleFlowView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiDoubleFlowQuery(HuaweiRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<DoubleFlowView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteDoubleFlowQuery(ZteRepository, eNodebId, sectorId);
        }

        private List<DoubleFlowView> QueryTopViewsByPolicy(IEnumerable<DoubleFlowView> source, int topCount,
            OrderDoubleFlowPolicy policy)
        {
            switch (policy)
            {
                case OrderDoubleFlowPolicy.OrderByCloseLoopDoubleFlowRate:
                    return source.Where(x => x.CloseLoopPrbs > 0).OrderBy(x => x.CloseLoopDoubleFlowRate).Take(topCount)
                        .ToList();
                case OrderDoubleFlowPolicy.OrderByDoubleFlowRate:
                    return source.OrderBy(x => x.DoubleFlowRate).Take(topCount).ToList();
                case OrderDoubleFlowPolicy.OrderByOpenLoopDoubleFlowRate:
                    return source.Where(x => x.OpenLoopPrbs > 0).OrderBy(x => x.OpenLoopDoubleFlowRate).Take(topCount)
                        .ToList();
                case OrderDoubleFlowPolicy.OrderByRank1PrbsDescendings:
                    return source.OrderBy(x => x.Rank1Prbs).Take(topCount).ToList();
            }
            return new List<DoubleFlowView>();
        }

        public List<DoubleFlowView> QueryTopDistrictViews(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            results = results.FilterSinglePortCells(_cellRepository);
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.TotalPrbs).Take(topCount * days).ToList();
        }

        public List<DoubleFlowView> QueryTopDistrictViews(DateTime begin, DateTime end, int topCount,
            OrderDoubleFlowPolicy policy)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end); 
            var joinViews = zteStats.MapTo<IEnumerable<DoubleFlowView>>()
                .Concat(huaweiStats.MapTo<IEnumerable<DoubleFlowView>>()).ToList();
            joinViews = joinViews.FilterSinglePortCells(_cellRepository);
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<DoubleFlowView> QueryTopDistrictViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderDoubleFlowPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            joinViews = joinViews.FilterSinglePortCells(_cellRepository);
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}
