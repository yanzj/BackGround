using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Entities.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("专业市场MRO覆盖查询控制器")]
    [ApiGroup("专题优化")]
    public class MarketMroRsrpController : ApiController
    {
        private readonly TopCoverageStatService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _marketService;
        private readonly TownCoverageService _townMroRsrpService;
        private readonly CoverageStatService _coverageStatService;

        public MarketMroRsrpController(TopCoverageStatService service, CollegeCellViewService collegeCellViewService,
            HotSpotService marketService, TownCoverageService townMroRsrpService,
            CoverageStatService coverageStatService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _marketService = marketService;
            _townMroRsrpService = townMroRsrpService;
            _coverageStatService = coverageStatService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有专业市场MRO覆盖情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有专业市场天平均MRO覆盖统计")]
        public IEnumerable<AggregateCoverageView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var stats = _townMroRsrpService.QueryTownViews(startDate, lastDate, college.Id,
                    FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateCoverageView>()
                    : new AggregateCoverageView();
                result.Name = college.HotspotName;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有专业市场指定日期范围内MRO覆盖情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("MRO覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateCoverageView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateCoverageView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townMroRsrpService.QueryOneDateBandStat(begin, FrequencyBandType.Market);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateCoverageView>();
                item.StatDate = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有专业市场MRO覆盖情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有专业市场天MRO覆盖统计")]
        public IEnumerable<AggregateCoverageView> Get(DateTime currentDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townMroRsrpService.QueryTownViews(currentDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateCoverageView>()
                    : new AggregateCoverageView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内MRO覆盖情况")]
        [ApiParameterDoc("marketName", "专业市场名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均MRO覆盖统计")]
        public AggregateCoverageView Get(string marketName, DateTime begin, DateTime end)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townMroRsrpService.QueryTownViews(begin, end, college.Id, FrequencyBandType.Market);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateCoverageView>()
                : new AggregateCoverageView();
            result.Name = marketName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内MRO覆盖情况，按照日期排列")]
        [ApiParameterDoc("marketName", "专业市场名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("MRO覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateCoverageView> GetDateViews(string marketName, DateTime beginDate, DateTime endDate)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townMroRsrpService.QueryTownViews(beginDate, endDate, college.Id, FrequencyBandType.Market);
            var results = stats.MapTo<List<AggregateCoverageView>>();
            results.ForEach(view =>
            {
                view.Name = marketName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期各个小区MRO覆盖情况")]
        [ApiParameterDoc("marketName", "专业市场名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区MRO覆盖情况统计")]
        public IEnumerable<CoverageStatView> GetMarketDateView(string marketName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return new List<CoverageStatView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
                {
                    var items = _service.GetDateSpanViews(cell.ENodebId, cell.SectorId, beginDate, endDate).ToList();
                    items.ForEach(item => { cell.MapTo(item); });
                    return items;
                })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<CoverageStatView>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<CoverageStatView>() : viewList;
        }
        
        [ApiDoc("更新指定日期的MRO覆盖率校园统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiResponse("成功更新记录数量")]
        [HttpGet]
        public async Task<int> RetrieveAndUpdate(DateTime statTime)
        {
            var beginDate = statTime.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var stats = _coverageStatService.QueryStats(beginDate, endDate);
            var results = colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList
                    = (from c in cells
                        join s in stats on new {c.ENodebId, c.SectorId} equals new { s.ENodebId, s.SectorId}
                        select s).ToList();
                if (!viewListList.Any()) return null;
                var stat = viewListList.MapTo<List<TownCoverageStat>>().ArraySum();
                stat.FrequencyBandType = FrequencyBandType.Market;
                stat.TownId = college.Id;
                stat.Id = 0;
                return stat;
            }).Where(x => x != null).ToList();
            return await _townMroRsrpService.DumpTownStats(results);
        }

    }
}