using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.RegionKpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.College
{
    [ApiControl("交通枢纽流量查询控制器")]
    [ApiGroup("专题优化")]
    public class TransportationFlowController : ApiController
    {
        private readonly FlowQueryService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _transportationService;
        private readonly TownFlowService _townFlowService;

        public TransportationFlowController(FlowQueryService service, CollegeCellViewService collegeCellViewService,
            HotSpotService transportationService, TownFlowService townFlowService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _transportationService = transportationService;
            _townFlowService = townFlowService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内所有交通枢纽流量情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有交通枢纽天平均流量统计")]
        public IEnumerable<AggregateFlowView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var stats = _townFlowService.QueryTownFlowViews(startDate, lastDate, college.Id, FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateFlowView>()
                    : new AggregateFlowView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询所有交通枢纽指定日期范围内流量情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("流量情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateFlowView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateFlowView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townFlowService.QueryOneDateBandStat(begin, FrequencyBandType.Transportation);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateFlowView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有交通枢纽流量情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有交通枢纽天流量统计")]
        public IEnumerable<AggregateFlowView> Get(DateTime currentDate)
        {
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townFlowService.QueryTownFlowViews(currentDate, lastDate, college.Id, FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateFlowView>()
                    : new AggregateFlowView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内流量情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均流量统计")]
        public AggregateFlowView Get(string transportationName, DateTime begin, DateTime end)
        {
            var college = _transportationService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townFlowService.QueryTownFlowViews(begin, end, college.Id, FrequencyBandType.Transportation);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateFlowView>()
                : new AggregateFlowView();
            result.Name = transportationName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内流量情况，按照日期排列")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("流量情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateFlowView> GetDateViews(string transportationName, DateTime beginDate, DateTime endDate)
        {
            var college = _transportationService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townFlowService.QueryTownFlowViews(beginDate, endDate, college.Id, FrequencyBandType.Transportation);
            var results = stats.MapTo<List<AggregateFlowView>>();
            results.ForEach(view =>
            {
                view.Name = transportationName;
            });
            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期各个小区流量情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区流量情况统计")]
        public IEnumerable<SectorFlowView> GetTransportationDateFlowView(string transportationName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _transportationService.QueryTransportationView(transportationName);
            if (college == null) return new List<SectorFlowView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
            {
                var items = _service.Query(cell.ENodebId, cell.SectorId, beginDate, endDate)
                    .MapTo<List<SectorFlowView>>();
                items.ForEach(item => { cell.MapTo(item); });
                return items;
            })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<SectorFlowView>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<SectorFlowView>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有交通枢纽的流量统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有交通枢纽的流量统计")]
        public IEnumerable<TownFlowStat> GetDateFlowView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList = cells.Select(cell => _service.Query(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownFlowStat>();
                stat.FrequencyBandType = FrequencyBandType.Transportation;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}