using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Support.View;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("MRS覆蓋率查询控制器")]
    [ApiGroup("KPI")]
    public class MrsRegionController : ApiController
    {
        private readonly MrsRegionStatService _service;

        public MrsRegionController(MrsRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域MRS覆蓋率")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域MRS覆蓋率")]
        public MrsRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域MRS覆蓋率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域MRS覆蓋率列表")]
        public IEnumerable<MrsRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }
    }
}
