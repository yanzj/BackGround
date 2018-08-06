using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Support;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("区域RRC连接成功率查询控制器")]
    [ApiGroup("KPI")]
    public class RrcRegionController : ApiController
    {
        private readonly RrcRegionStatService _service;

        public RrcRegionController(RrcRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域RRC连接成功率")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域RRC连接成功率")]
        public RrcRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域RRC连接成功率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域RRC连接成功率列表")]
        public IEnumerable<RrcRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }
    }
}