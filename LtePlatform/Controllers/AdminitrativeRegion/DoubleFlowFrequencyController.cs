﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Support.View;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("分频段查询双流比控制器")]
    [ApiGroup("KPI")]
    public class DoubleFlowFrequencyController : ApiController
    {
        private readonly DoubleFlowRegionStatService _service;

        public DoubleFlowFrequencyController(DoubleFlowRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的分频段双流比列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段双流比列表")]
        public IEnumerable<DoubleFlowRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            return _service.QueryCityFrequencyViews(begin, end, city);
        }

        [HttpGet]
        [ApiDoc("查询指定区域和时间段的分频段双流比列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段双流比列表")]
        public IEnumerable<DoubleFlowRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            return _service.QueryDistrictFrequencyViews(begin, end, city, district);
        }

        [HttpGet]
        [ApiDoc("查询指定镇和时间段的分频段双流比列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段双流比列表")]
        public IEnumerable<DoubleFlowRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            return _service.QueryTownFrequencyViews(begin, end, city, district, town);
        }
    }
}