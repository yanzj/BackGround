using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("校园网流量查询控制器")]
    [ApiGroup("专题优化")]
    public class CollegeFlowController : ApiController
    {
        private readonly FlowQueryService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;
        private readonly TownFlowService _townFlowService;

        public CollegeFlowController(FlowQueryService service, CollegeCellViewService collegeCellViewService,
            CollegeStatService collegeService, TownFlowService townFlowService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
            _townFlowService = townFlowService;
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内流量情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均流量统计")]
        public AggregateFlowView Get(string collegeName, DateTime begin, DateTime end)
        {
            var college = _collegeService.QueryInfo(collegeName);
            var cells = _collegeCellViewService.QueryCollegeSectors(collegeName);
            if (college == null) return null;
            var stats = _townFlowService.QueryTownFlowViews(begin, end, college.Id, FrequencyBandType.College);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateFlowView>()
                : new AggregateFlowView();
            result.CellCount = cells.Count();
            result.Name = collegeName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内流量情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("流量情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateFlowView> GetDateViews(string collegeName, DateTime beginDate, DateTime endDate)
        {
            var college = _collegeService.QueryInfo(collegeName);
            var cells = _collegeCellViewService.QueryCollegeSectors(collegeName);
            if (college == null) return null;
            var stats = _townFlowService.QueryTownFlowViews(beginDate, endDate, college.Id, FrequencyBandType.College);
            var results = stats.MapTo<List<AggregateFlowView>>();
            results.ForEach(view =>
            {
                view.CellCount = cells.Count();
                view.Name = collegeName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定学校指定日期各个小区流量情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区流量情况统计")]
        public IEnumerable<SectorFlowView> GetCollegeDateFlowView(string collegeName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return new List<SectorFlowView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.Name);
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
        [ApiDoc("抽取查询单日所有校园网的流量统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有校园网的流量统计")]
        public IEnumerable<TownFlowStat> GetDateFlowView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList = cells.Select(cell => _service.Query(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownFlowStat>();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}