﻿using Lte.Domain.Common.Wireless.Cell;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("镇级CQI优良比查询控制器")]
    [ApiGroup("KPI")]
    public class TownCqiController : ApiController
    {
        private readonly TownCqiService _service;

        public TownCqiController(TownCqiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期前最近一天有记录的镇级天CQI优良比")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("镇级天CQI优良比，每个镇一条记录")]
        public IEnumerable<TownCqiView> Get(DateTime statDate, string frequency)
        {
            return _service.QueryLastDateView(statDate, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期前最近一天有记录的镇级天CQI优良比")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区名称")]
        [ApiResponse("镇级天CQI优良比，每个镇一条记录")]
        public IEnumerable<TownCqiView> Get(DateTime statDate, string city, string district, string frequency)
        {
            return _service.QueryLastDateView(statDate, city, district, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期有记录的镇级天CQI优良比")]
        [ApiParameterDoc("currentDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("镇级天CQI优良比，每个镇一条记录")]
        public IEnumerable<TownCqiStat> GetCurrentDate(DateTime currentDate, string frequency)
        {
            return _service.QueryCurrentDateStats(currentDate, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询时间段内区域天CQI优良比统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("天CQI优良比统计列表")]
        public IEnumerable<CqiRegionDateView> Get(DateTime begin, DateTime end, string city, string frequency)
        {
            return _service.QueryDateSpanStats(begin, end, city, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询时间段内镇天CQI优良比统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("天CQI优良比统计列表")]
        public IEnumerable<TownCqiView> Get(DateTime begin, DateTime end, string city, string district, string town)
        {
            return _service.QueryDateSpanViews(begin, end, city, district, town);
        }

        [HttpGet]
        [ApiDoc("查询时间段内镇天CQI优良比统计/按頻點匯總")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("天CQI优良比统计列表")]
        public IEnumerable<TownCqiView> GetGroup(DateTime beginDate, DateTime endDate, string city, string district, string town)
        {
            return _service.QueryDateSpanGroupByFrequency(beginDate, endDate, city, district, town);
        }

        [HttpPost]
        public TownCqiStat Post(TownCqiStat stat)
        {
            return _service.Update(stat);
        }
    }
}