﻿using Abp.EntityFramework.AutoMapper;
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
    [ApiControl("交通枢纽忙时CQI优良比查询控制器")]
    [ApiGroup("专题优化")]
    public class TransportationHourCqiController : ApiController
    {
        private readonly HourCqiService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly HotSpotService _transportationService;
        private readonly TownHourCqiService _townCqiService;

        public TransportationHourCqiController(HourCqiService service, CollegeCellViewService collegeCellViewService,
            HotSpotService transportationService, TownHourCqiService townCqiService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _transportationService = transportationService;
            _townCqiService = townCqiService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内所有交通枢纽忙时CQI优良比情况")]
        [ApiParameterDoc("startDate", "开始日期")]
        [ApiParameterDoc("lastDate", "结束日期")]
        [ApiResponse("所有交通枢纽天平均CQI优良比统计")]
        public IEnumerable<AggregateHourCqiView> Get(DateTime startDate, DateTime lastDate)
        {
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var stats = _townCqiService.QueryTownCqiViews(startDate, lastDate, college.Id, FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                    : new AggregateHourCqiView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询所有交通枢纽指定日期范围内忙时CQI优良比情况，按照日期排列")]
        [ApiParameterDoc("firstDate", "开始日期")]
        [ApiParameterDoc("secondDate", "结束日期")]
        [ApiResponse("CQI优良比情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateHourCqiView> GetAllDateViews(DateTime firstDate, DateTime secondDate)
        {
            var results = new List<AggregateHourCqiView>();
            var begin = firstDate;
            while (begin <= secondDate)
            {
                var stat = _townCqiService.QueryOneDateBandStat(begin, FrequencyBandType.Transportation);
                begin = begin.AddDays(1);
                if (stat == null) continue;
                var item = stat.MapTo<AggregateHourCqiView>();
                item.StatTime = begin.AddDays(-1);
                results.Add(item);
            }

            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定日期内所有交通枢纽忙时CQI优良比情况")]
        [ApiParameterDoc("currentDate", "指定日期")]
        [ApiResponse("所有交通枢纽天CQI优良比统计")]
        public IEnumerable<AggregateHourCqiView> Get(DateTime currentDate)
        {
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            var lastDate = currentDate.AddDays(1);
            return colleges.Select(college =>
            {
                var stats = _townCqiService.QueryTownCqiViews(currentDate, lastDate, college.Id, FrequencyBandType.Transportation);
                var result = stats.Any()
                    ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                    : new AggregateHourCqiView();
                result.Name = college.HotspotName;
                return result;
            });
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内忙时CQI优良比情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("天平均CQI优良比统计")]
        public AggregateHourCqiView Get(string transportationName, DateTime begin, DateTime end)
        {
            var college = _transportationService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townCqiService.QueryTownCqiViews(begin, end, college.Id, FrequencyBandType.Transportation);
            var result = stats.Any()
                ? stats.ArraySum().MapTo<AggregateHourCqiView>()
                : new AggregateHourCqiView();
            result.Name = transportationName;
            return result;
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期范围内忙时CQI优良比情况，按照日期排列")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("CQI优良比情况，按照日期排列，每天一条记录")]
        public IEnumerable<AggregateHourCqiView> GetDateViews(string transportationName, DateTime beginDate, DateTime endDate)
        {
            var college = _transportationService.QueryTransportationView(transportationName);
            if (college == null) return null;
            var stats = _townCqiService.QueryTownCqiViews(beginDate, endDate, college.Id, FrequencyBandType.Transportation);
            var results = stats.MapTo<List<AggregateHourCqiView>>();
            results.ForEach(view =>
            {
                view.Name = transportationName;
            });
            return results;
        }

        [HttpGet]
        [ApiDoc("查询指定交通枢纽指定日期各个小区忙时CQI优良比情况")]
        [ApiParameterDoc("transportationName", "交通枢纽名称")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("各个小区CQI优良比情况统计")]
        public IEnumerable<HourCqiView> GetTransportationDateCqiView(string transportationName, DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var college = _transportationService.QueryTransportationView(transportationName);
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
        [ApiDoc("抽取查询单日所有交通枢纽的忙时CQI优良比统计（导入采用，一般前端代码不要用这个接口）")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("所有交通枢纽的CQI优良比统计")]
        public IEnumerable<TownHourCqi> GetDateCqiView(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _transportationService.QueryHotSpotViews("交通枢纽");
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.HotspotName);
                var viewListList = cells.Select(cell => _service.QueryHourCqiViews(cell.ENodebId, cell.SectorId, beginDate, endDate))
                    .Where(views => views != null && views.Any()).ToList();
                if (!viewListList.Any()) return null;
                var viewList = viewListList.Aggregate((x, y) => x.Concat(y).ToList()).ToList();
                if (!viewList.Any()) return null;
                var stat = viewList.ArraySum().MapTo<TownHourCqi>();
                stat.FrequencyBandType = FrequencyBandType.Transportation;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);

        }
    }
}