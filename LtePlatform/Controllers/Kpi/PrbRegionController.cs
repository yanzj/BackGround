using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("区域PRB利用率查询控制器")]
    [ApiGroup("KPI")]
    public class PrbRegionController : ApiController
    {
        private readonly PrbRegionStatService _service;

        public PrbRegionController(PrbRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域PRB利用率")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域PRB利用率")]
        public PrbRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域PRB利用率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域PRB利用率列表")]
        public IEnumerable<PrbRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }
    }
}
