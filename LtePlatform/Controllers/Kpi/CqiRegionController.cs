using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("区域CQI优良比查询控制器")]
    [ApiGroup("KPI")]
    public class CqiRegionController : ApiController
    {
        private readonly CqiRegionStatService _service;

        public CqiRegionController(CqiRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域CQI优良比")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域CQI优良比")]
        public QciRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域CQI优良比列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域CQI优良比列表")]
        public IEnumerable<QciRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的集团口径区域CQI优良比")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statTime", "日期")]
        [ApiResponse("集团口径区域CQI优良比")]
        public CqiRegionDateView GetCqi(string city, DateTime statTime)
        {
            return _service.QueryLastDateCqi(statTime, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的集团口径区域CQI优良比列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("集团口径区域CQI优良比列表")]
        public IEnumerable<CqiRegionDateView> GetCqi(DateTime beginDate, DateTime endDate, string city)
        {
            return _service.QueryDateCqis(beginDate, endDate, city);
        }
    }
}