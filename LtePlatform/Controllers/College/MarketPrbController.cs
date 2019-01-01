using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("专业市场PRB利用率查询控制器")]
    [ApiGroup("专题优化")]
    public class MarketPrbController : ApiController
    {
        private readonly PrbQueryService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _marketService;
        private readonly TownPrbService _townPrbService;

        public MarketPrbController(PrbQueryService service, CollegeCellViewService collegeCellViewService,
            HotSpotService marketService, TownPrbService townPrbService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _marketService = marketService;
            _townPrbService = townPrbService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有专业市场PRB利用率情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有专业市场天平均PRB利用率统计")]
        public IEnumerable<AggregatePrbView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var stats = _townPrbService.QueryTownPrbViews(startDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePrbView>()
                    : new AggregatePrbView();
                result.Name = college.HotspotName;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有专业市场指定日期范围内PRB利用率情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("PRB利用率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePrbView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregatePrbView>();
            var begin = firstDate;
            while (begin<=secondDate)
            {
                var stat = _townPrbService.QueryOneDateBandStat(begin, FrequencyBandType.Market);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregatePrbView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有专业市场PRB利用率情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有专业市场天PRB利用率统计")]
        public IEnumerable<AggregatePrbView> Get(DateTime currentDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townPrbService.QueryTownPrbViews(currentDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePrbView>()
                    : new AggregatePrbView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内PRB利用率情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均PRB利用率统计")]
        public AggregatePrbView Get(string marketName, DateTime begin, DateTime end)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townPrbService.QueryTownPrbViews(begin, end, college.Id, FrequencyBandType.Market);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregatePrbView>()
                : new AggregatePrbView();
            result.Name = marketName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内PRB利用率情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("PRB利用率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePrbView> GetDateViews(string marketName, DateTime beginDate, DateTime endDate)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townPrbService.QueryTownPrbViews(beginDate, endDate, college.Id, FrequencyBandType.Market);
            var results = stats.MapTo<List<AggregatePrbView>>();
            results.ForEach(view =>
            {
                view.Name = marketName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期各个小区PRB利用率情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区PRB利用率情况统计")]
        public IEnumerable<PrbView> GetMarketDatePrbView(string marketName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return new List<PrbView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
                {
                    var items = _service.Query(cell.ENodebId, cell.SectorId, beginDate, endDate);
                    items.ForEach(item => { cell.MapTo(item); });
                    return items;
                })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<PrbView>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<PrbView>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有专业市场的PRB利用率统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有专业市场的PRB利用率统计")]
        public IEnumerable<TownPrbStat> GetDatePrbView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList = cells.Select(cell => _service.Query(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownPrbStat>();
                stat.FrequencyBandType = FrequencyBandType.Market;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}