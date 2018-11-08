using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("校园网MRS-TA覆盖查询控制器")]
    [ApiGroup("专题优化")]
    public class CollegeMrsTadvController : ApiController
    {
        private readonly MrsService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;
        private readonly TownMrsTadvService _townMrsTadvService;

        public CollegeMrsTadvController(MrsService service, CollegeCellViewService collegeCellViewService,
            CollegeStatService collegeService, TownMrsTadvService townMrsTadvService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
            _townMrsTadvService = townMrsTadvService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内所有学校MRS-Tadv覆盖情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有学校天平均MRS-Tadv覆盖统计")]
        public IEnumerable<AggregateMrsTadvView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _collegeService.QueryInfos();
            return colleges.Select(college =>
            {
                var stats = _townMrsTadvService.QueryTownViews(startDate, lastDate, college.Id, FrequencyBandType.College);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateMrsTadvView>()
                    : new AggregateMrsTadvView();
                result.Name = college.Name;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询所有学校指定日期范围内MRS-Tadv覆盖情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("MRS-Tadv覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateMrsTadvView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateMrsTadvView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townMrsTadvService.QueryOneDateBandStat(begin, FrequencyBandType.College);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateMrsTadvView>();
                item.StatDate = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有学校MRS-Tadv覆盖情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有学校天MRS-Tadv覆盖统计")]
        public IEnumerable<AggregateMrsTadvView> Get(DateTime currentDate)
        {
            var colleges = _collegeService.QueryInfos();
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townMrsTadvService.QueryTownViews(currentDate, lastDate, college.Id, FrequencyBandType.College);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateMrsTadvView>()
                    : new AggregateMrsTadvView();
                result.Name = college.Name;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内MRS-Tadv覆盖情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均MRS-Tadv覆盖统计")]
        public AggregateMrsTadvView Get(string collegeName, DateTime begin, DateTime end)
        {
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return null;
            var stats = _townMrsTadvService.QueryTownViews(begin, end, college.Id, FrequencyBandType.College);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateMrsTadvView>()
                : new AggregateMrsTadvView();
            result.Name = collegeName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内MRS-Tadv覆盖情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("MRS-Tadv覆盖情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateMrsTadvView> GetDateViews(string collegeName, DateTime beginDate, DateTime endDate)
        {
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return null;
            var stats = _townMrsTadvService.QueryTownViews(beginDate, endDate, college.Id, FrequencyBandType.College);
            var results = stats.MapTo<List<AggregateMrsTadvView>>();
            results.ForEach(view =>
            {
                view.Name = collegeName;
            });
            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期各个小区MRS-Tadv覆盖情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区MRS-Tadv覆盖情况统计")]
        public IEnumerable<CellMrsTadvDto> GetCollegeDateView(string collegeName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return new List<CellMrsTadvDto>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.Name);
            var viewListList = cells.Select(cell =>
            {
                var items = _service.QueryMrsTadvStats(cell.ENodebId, cell.SectorId, beginDate, endDate).ToList();
                items.ForEach(item => { cell.MapTo(item); });
                return items;
            })
                .Where(views => views.Any()).ToList();
            if (!viewListList.Any()) return new List<CellMrsTadvDto>();
            var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList());
            return !viewList.Any() ? new List<CellMrsTadvDto>() : viewList;
        }

        [HttpGet]
        [ApiDoc("抽取查询单日所有校园网的MRS-Tadv覆盖统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有校园网的MRS-Tadv覆盖统计")]
        public IEnumerable<TownMrsTadvDto> GetDateView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList = cells.Select(cell =>
                        _service.QueryTadvStats(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList()).ToList();
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownMrsTadvDto>();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}