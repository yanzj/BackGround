﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("校园网精确覆盖率查询控制器")]
    [ApiGroup("专题优化")]
    public class CollegePreciseController : ApiController
    {
        private readonly PreciseStatService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;
        private readonly TownPreciseService _townMrsRsrpService;

        public CollegePreciseController(PreciseStatService service, CollegeCellViewService collegeCellViewService,
            CollegeStatService collegeService, TownPreciseService townMrsRsrpService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
            _townMrsRsrpService = townMrsRsrpService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内所有学校精确覆盖率情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有学校天平均精确覆盖率统计")]
        public IEnumerable<AggregatePreciseView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _collegeService.QueryInfos();
            return colleges.Select(college =>
            {
                var stats = _townMrsRsrpService.QueryTownViews(startDate, lastDate, college.Id, FrequencyBandType.College);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePreciseView>()
                    : new AggregatePreciseView();
                result.Name = college.Name;
                return result;
            });
        }
        
        [HttpGet]
        [ApiDoc("查询所有学校指定日期范围内精确覆盖率情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("精确覆盖率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePreciseView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregatePreciseView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townMrsRsrpService.QueryOneDateBandStat(begin, FrequencyBandType.College);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregatePreciseView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有学校精确覆盖率情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有学校天精确覆盖率统计")]
        public IEnumerable<AggregatePreciseView> Get(DateTime currentDate)
        {
            var colleges = _collegeService.QueryInfos();
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townMrsRsrpService.QueryTownViews(currentDate, lastDate, college.Id, FrequencyBandType.College);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregatePreciseView>()
                    : new AggregatePreciseView();
                result.Name = college.Name;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内精确覆盖率情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均精确覆盖率统计")]
        public AggregatePreciseView Get(string collegeName, DateTime begin, DateTime end)
        {
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return null;
            var stats = _townMrsRsrpService.QueryTownViews(begin, end, college.Id, FrequencyBandType.College);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregatePreciseView>()
                : new AggregatePreciseView();
            result.Name = collegeName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定学校指定日期范围内精确覆盖率情况，按照日期排列")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("精确覆盖率情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregatePreciseView> GetDateViews(string collegeName, DateTime beginDate, DateTime endDate)
        {
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return null;
            var stats = _townMrsRsrpService.QueryTownViews(beginDate, endDate, college.Id, FrequencyBandType.College);
            var results = stats.MapTo<List<AggregatePreciseView>>();
            results.ForEach(view =>
            {
                view.Name = collegeName;
            });
            return results;
        }
        
        [HttpGet]
        [ApiDoc("查询指定学校指定日期各个小区精确覆盖率情况")]
        [ApiParameterDoc("collegeName", "学校名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区MRS覆盖情况统计")]
        public IEnumerable<Precise4GView> GetCollegeDateView(string collegeName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _collegeService.QueryInfo(collegeName);
            if (college == null) return new List<Precise4GView>();
            var cells = _collegeCellViewService.QueryCollegeSectors(college.Name);
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
        [ApiDoc("抽取查询单日所有校园网的精确覆盖率统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有校园网的精确覆盖率统计")]
        public IEnumerable<TownPreciseView> GetDateView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList = cells.Select(cell =>
                        _service.GetTimeSpanStats(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList()).ToList();
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownPreciseView>();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}