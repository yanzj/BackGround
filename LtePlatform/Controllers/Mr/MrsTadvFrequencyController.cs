﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Support.View;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("分频段查询MRS-TA指标控制器")]
    [ApiGroup("MR")]
    public class MrsTadvFrequencyController : ApiController
    {
        private readonly TadvRegionStatService _service;

        public MrsTadvFrequencyController(TadvRegionStatService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("查询指定城市和时间段的分频段MRS-TA列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段MRS-TA列表")]
        public IEnumerable<MrsTadvRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            return _service.QueryCityFrequencyViews(begin, end, city);
        }
        
        [HttpGet]
        [ApiDoc("查询指定区域和时间段的分频段MRS-TA列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段MRS-TA列表")]
        public IEnumerable<MrsTadvRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            return _service.QueryDistrictFrequencyViews(begin, end, city, district);
        }
        
        [HttpGet]
        [ApiDoc("查询指定镇和时间段的分频段MRS-TA列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段MRS-TA列表")]
        public IEnumerable<MrsTadvRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            return _service.QueryTownFrequencyViews(begin, end, city, district, town);
        }
    }
}