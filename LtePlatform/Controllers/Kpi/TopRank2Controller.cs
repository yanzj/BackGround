using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP双流比查询控制器")]
    [ApiGroup("KPI")]
    public class TopRank2Controller : ApiController
    {
        private readonly FlowQueryService _service;
        private readonly ENodebQueryService _eNodebQueryServicee;

        public TopRank2Controller(FlowQueryService service, ENodebQueryService eNodebQueryServicee)
        {
            _service = service;
            _eNodebQueryServicee = eNodebQueryServicee;
        }

        [HttpGet]
        [ApiDoc("查询指定区域指定时间范围内TOP双流比小区指标统计")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("TOP双流比小区指标统计，按小区排列")]
        public IEnumerable<FlowView> Get(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var views = _service.QueryTopRank2Views(city, district, begin, end, topCount).ToList();
            views.ForEach(view =>
            {
                var eNodeb = _eNodebQueryServicee.GetByENodebId(view.ENodebId);
                view.ENodebName = eNodeb?.Name;
                view.City = city;
                view.District = district;
                view.Town = eNodeb?.TownName;
            });
            return views;
        }

        [HttpGet]
        [ApiDoc("查询指定时间范围内TOP双流比小区指标统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("TOP双流比小区指标统计，按小区排列")]
        public IEnumerable<FlowView> Get(DateTime begin, DateTime end, int topCount)
        {
            var views = _service.QueryAllTopRank2Views(begin, end, topCount).ToList();
            views.ForEach(view =>
            {
                var eNodeb = _eNodebQueryServicee.GetByENodebId(view.ENodebId);
                view.ENodebName = eNodeb?.Name;
                view.City = eNodeb?.CityName;
                view.District = eNodeb?.DistrictName;
                view.Town = eNodeb?.TownName;
            });
            return views;
        }
    }
}