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
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("专业市场忙时CQI优良比查询控制器")]
    [ApiGroup("专题优化")]
    public class MarketHourCqiController : ApiController
    {
        private readonly HourCqiService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _marketService;
        private readonly TownHourCqiService _townCqiService;

        public MarketHourCqiController(HourCqiService service, CollegeCellViewService collegeCellViewService,
            HotSpotService marketService, TownHourCqiService townCqiService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _marketService = marketService;
            _townCqiService = townCqiService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有专业市场忙时CQI优良比情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有专业市场天平均CQI优良比统计")]
        public IEnumerable<AggregateHourCqiView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var stats = _townCqiService.QueryTownCqiViews(startDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                    : new AggregateHourCqiView();
                result.Name = college.HotspotName;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有专业市场指定日期范围内忙时CQI优良比情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("CQI优良比情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateHourCqiView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateHourCqiView>();
            var begin = firstDate;
            while (begin<=secondDate)
            {
                var stat = _townCqiService.QueryOneDateBandStat(begin, FrequencyBandType.Market);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateHourCqiView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有专业市场忙时CQI优良比情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有专业市场天CQI优良比统计")]
        public IEnumerable<AggregateHourCqiView> Get(DateTime currentDate)
        {
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townCqiService.QueryTownCqiViews(currentDate, lastDate, college.Id, FrequencyBandType.Market);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                    : new AggregateHourCqiView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内忙时CQI优良比情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均CQI优良比统计")]
        public AggregateHourCqiView Get(string marketName, DateTime begin, DateTime end)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townCqiService.QueryTownCqiViews(begin, end, college.Id, FrequencyBandType.Market);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                : new AggregateHourCqiView();
            result.Name = marketName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期范围内忙时CQI优良比情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("CQI优良比情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateHourCqiView> GetDateViews(string marketName, DateTime beginDate, DateTime endDate)
        {
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return null;
            var stats = _townCqiService.QueryTownCqiViews(beginDate, endDate, college.Id, FrequencyBandType.Market);
            var results = stats.MapTo<List<AggregateHourCqiView>>();
            results.ForEach(view =>
            {
                view.Name = marketName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定专业市场指定日期各个小区忙时CQI优良比情况")]
        [ApiParameterDoc("collegeName", "专业市场名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区CQI优良比情况统计")]
        public IEnumerable<HourCqiView> GetMarketDateCqiView(string marketName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _marketService.QueryMarketView(marketName);
            if (college == null) return new List<HourCqiView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
                {
                    var items = _service.QueryHourCqiViews(cell.ENodebId, cell.SectorId, beginDate, endDate).ToList();
                    items.ForEach(item => { cell.MapTo(item); });
                    return items;
                })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<HourCqiView>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<HourCqiView>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有专业市场的忙时CQI优良比统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有专业市场的CQI优良比统计")]
        public IEnumerable<TownHourCqi> GetDateCqiView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _marketService.QueryHotSpotViews("专业市场");
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList = cells.Select(cell => _service.QueryHourCqiViews(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList()).ToList();
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownHourCqi>();
                stat.FrequencyBandType = FrequencyBandType.Market;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}