using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("MRO覆蓋率查询控制器")]
    [ApiGroup("KPI")]
    public class CoverageRegionController : ApiController
    {
        private readonly CoverageRegionStatService _service;

        public CoverageRegionController(CoverageRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域MRO覆蓋率")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域MRS覆蓋率")]
        public CoverageRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域MRO覆蓋率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域MRS覆蓋率列表")]
        public IEnumerable<CoverageRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }
    }
}
