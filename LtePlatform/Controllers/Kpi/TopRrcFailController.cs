using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP RRC连接失败小区查询控制器")]
    [ApiGroup("KPI")]
    public class TopRrcFailController : ApiController
    {
        private readonly RrcQueryService _service;

        public TopRrcFailController(RrcQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定区域指定时间范围内TOP RRC连接失败小区指标统计")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("TOP RRC连接失败小区指标统计，按小区排列")]
        public IEnumerable<RrcView> Get(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            return _service.QueryTopRrcFailViews(city, district, begin, end, topCount);
        }
    }
}