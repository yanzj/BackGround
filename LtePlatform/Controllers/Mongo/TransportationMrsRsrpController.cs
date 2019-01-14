using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("交通枢纽MRS覆盖查询控制器")]
    [ApiGroup("专题优化")]
    public class TransportationMrsRsrpController : ApiController
    {
        private readonly MrsService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _marketService;
        private readonly TownMrsRsrpService _townMrsRsrpService;

        public TransportationMrsRsrpController(MrsService service, CollegeCellViewService collegeCellViewService,
            HotSpotService marketService, TownMrsRsrpService townMrsRsrpService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _marketService = marketService;
            _townMrsRsrpService = townMrsRsrpService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有交通枢纽MRS覆盖情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有交通枢纽天平均MRS覆盖统计")]
        public IEnumerable<AggregateMrsRsrpView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _marketService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var stats = _townMrsRsrpService.QueryTownViews(startDate, lastDate, college.Id,
                    FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateMrsRsrpView>()
                    : new AggregateMrsRsrpView();
                result.Name = college.HotspotName;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有交通枢纽指定日期范围内MRS覆盖情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("MRS覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateMrsRsrpView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateMrsRsrpView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townMrsRsrpService.QueryOneDateBandStat(begin, FrequencyBandType.Transportation);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateMrsRsrpView>();
                item.StatDate = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有交通枢纽MRS覆盖情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有交通枢纽天MRS覆盖统计")]
        public IEnumerable<AggregateMrsRsrpView> Get(DateTime currentDate)
        {
            var colleges = _marketService.QueryHotSpotViews("交通枢纽");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townMrsRsrpService.QueryTownViews(currentDate, lastDate, college.Id,
                    FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateMrsRsrpView>()
                    : new AggregateMrsRsrpView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内MRS覆盖情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均MRS覆盖统计")]
        public AggregateMrsRsrpView Get(string transportationName, DateTime begin, DateTime end)
        {
            var college = _marketService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townMrsRsrpService.QueryTownViews(begin, end, college.Id, FrequencyBandType.Transportation);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateMrsRsrpView>()
                : new AggregateMrsRsrpView();
            result.Name = transportationName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内MRS覆盖情况，按照日期排列")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("MRS覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateMrsRsrpView> GetDateViews(string transportationName, DateTime beginDate, DateTime endDate)
        {
            var college = _marketService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townMrsRsrpService.QueryTownViews(beginDate, endDate, college.Id, FrequencyBandType.Transportation);
            var results = stats.MapTo<List<AggregateMrsRsrpView>>();
            results.ForEach(view =>
            {
                view.Name = transportationName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期各个小区MRS覆盖情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区MRS覆盖情况统计")]
        public IEnumerable<CellMrsRsrpDto> GetTransportationDateView(string transportationName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _marketService.QueryTransportationView(transportationName);
            if (college == null) return new List<CellMrsRsrpDto>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.HotspotName);
            var viewListList = cells.Select(cell =>
                {
                    var items = _service.QueryRsrpStats(cell.ENodebId, cell.SectorId, beginDate, endDate).ToList();
                    items.ForEach(item => { cell.MapTo(item); });
                    return items;
                })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<CellMrsRsrpDto>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<CellMrsRsrpDto>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有交通枢纽的MRS覆盖统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有交通枢纽的MRS覆盖统计")]
        public IEnumerable<TownMrsRsrpDto> GetDateView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _marketService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList = cells.Select(cell =>
                        _service.QueryMrsRsrpStats(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList()).ToList();
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownMrsRsrpDto>();
                stat.FrequencyBandType = FrequencyBandType.Transportation;
                stat.TownId = college.Id;
                stat.StatDate = statDate.Date;
                return stat;
            }).Where(x => x != null);

        }
    }
}