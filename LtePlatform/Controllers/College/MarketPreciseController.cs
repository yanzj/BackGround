using System;
using System.Collections.Generic;
using System.Linq;
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
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("专业市场精确覆盖率查询控制器")]
    [ApiGroup("专题优化")]
    public class MarketPreciseController : ApiController
    {
        private readonly PreciseStatService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _marketService;
        private readonly TownPreciseService _townPreciseService;

        public MarketPreciseController(PreciseStatService service, CollegeCellViewService collegeCellViewService,
            HotSpotService marketService, TownPreciseService townPreciseService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _marketService = marketService;
            _townPreciseService = townPreciseService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有专业市场精确覆盖率情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有专业市场天平均精确覆盖率统计")]
        public IEnumerable<AggregatePreciseView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var stats = _townPreciseService.QueryTownViews(startDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePreciseView>()
                    : new AggregatePreciseView();
                result.Name = college.HotspotName;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有专业市场指定日期范围内精确覆盖率情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("精确覆盖率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePreciseView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregatePreciseView>();
            var begin = firstDate;
            while (begin<=secondDate)
            {
                var stat = _townPreciseService.QueryOneDateBandStat(begin, FrequencyBandType.Market);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregatePreciseView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有专业市场精确覆盖率情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有专业市场天精确覆盖率统计")]
        public IEnumerable<AggregatePreciseView> Get(DateTime currentDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townPreciseService.QueryTownViews(currentDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePreciseView>()
                    : new AggregatePreciseView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内精确覆盖率情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均精确覆盖率统计")]
        public AggregatePreciseView Get(string marketName, DateTime begin, DateTime end)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townPreciseService.QueryTownViews(begin, end, college.Id, FrequencyBandType.Market);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregatePreciseView>()
                : new AggregatePreciseView();
            result.Name = marketName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内精确覆盖率情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("精确覆盖率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePreciseView> GetDateViews(string marketName, DateTime beginDate, DateTime endDate)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townPreciseService.QueryTownViews(beginDate, endDate, college.Id, FrequencyBandType.Market);
            var results = stats.MapTo<List<AggregatePreciseView>>();
            results.ForEach(view =>
            {
                view.Name = marketName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期各个小区精确覆盖率情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区精确覆盖率情况统计")]
        public IEnumerable<Precise4GView> GetMarketDatePreciseView(string marketName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return new List<Precise4GView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
                {
                    var items = _service.GetTimeSpanViews(cell.ENodebId, cell.SectorId, beginDate, endDate).ToList();
                    items.ForEach(item => { cell.MapTo(item); });
                    return items;
                })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<Precise4GView>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<Precise4GView>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有专业市场的精确覆盖率统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有专业市场的精确覆盖率统计")]
        public IEnumerable<TownPreciseStat> GetDatePreciseView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var stats = _service.GetTimeSpanStats(beginDate, endDate);
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList
                    = (from c in cells
                        join s in stats on new {c.ENodebId, c.SectorId} equals new {ENodebId = s.CellId, s.SectorId}
                        select s).ToList();
                if (!viewListList.Any()) return null;
                var stat = viewListList.ArraySum().MapTo<TownPreciseStat>();
                stat.FrequencyBandType = FrequencyBandType.Market;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}