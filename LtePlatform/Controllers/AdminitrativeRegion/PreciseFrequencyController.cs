using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Support.View;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("分频段精确覆盖率查询控制器")]
    [ApiGroup("KPI")]
    public class PreciseFrequencyController : ApiController
    {
        private readonly PreciseRegionStatService _service;

        public PreciseFrequencyController(PreciseRegionStatService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("查询指定城市和时间段的分频段精确覆盖率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段精确覆盖率列表")]
        public IEnumerable<PreciseRegionFrequencyView> QueryCityFrequencyViews(DateTime begin, DateTime end,
            string city)
        {
            return _service.QueryCityFrequencyViews(begin, end, city);
        }
        
        [HttpGet]
        [ApiDoc("查询指定区域和时间段的分频段精确覆盖率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段精确覆盖率列表")]
        public IEnumerable<PreciseRegionFrequencyView> QueryDistrictFrequencyViews(DateTime begin, DateTime end,
            string city, string district)
        {
            return _service.QueryDistrictFrequencyViews(begin, end, city, district);
        }
        
        [HttpGet]
        [ApiDoc("查询指定镇和时间段的分频段精确覆盖率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分频段精确覆盖率列表")]
        public IEnumerable<PreciseRegionFrequencyView> QueryTownFrequencyViews(DateTime begin, DateTime end,
            string city, string district, string town)
        {
            return _service.QueryTownFrequencyViews(begin, end, city, district, town);
        }
    }
}