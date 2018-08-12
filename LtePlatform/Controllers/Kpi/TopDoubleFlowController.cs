using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP MIMO双流比小區查询控制器")]
    [ApiGroup("KPI")]
    public class TopDoubleFlowController : ApiController
    {
        private readonly DoubleFlowQueryService _service;
        private readonly ENodebQueryService _eNodebQueryService;

        public TopDoubleFlowController(DoubleFlowQueryService service, ENodebQueryService eNodebQueryService)
        {
            _service = service;
            _eNodebQueryService = eNodebQueryService;
        }

        [HttpGet]
        [ApiDoc("查询指定区域指定时间范围内TOP MIMO双流比小区指标统计")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("TOP MIMO双流比小区指标统计，按小区排列")]
        public IEnumerable<DoubleFlowView> Get(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = _service.QueryTopDistrictViews(city, district, begin, end, topCount);
            results.ForEach(x =>
            {
                var view = _eNodebQueryService.GetByENodebId(x.ENodebId);
                x.ENodebName = view?.Name;
                x.City = city;
                x.District = district;
                x.Town = view?.TownName;
            });
            return results;
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得TOP MIMO双流比小区列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiResponse("TOP MIMO双流比小区列表")]
        public IEnumerable<DoubleFlowView> Get(DateTime begin, DateTime end, int topCount, string orderSelection)
        {
            var results = _service.QueryTopDistrictViews(begin, end, topCount,
                orderSelection.GetEnumType<OrderDoubleFlowPolicy>());
            results.ForEach(x =>
            {
                var view = _eNodebQueryService.GetByENodebId(x.ENodebId);
                x.ENodebName = view?.Name;
                x.City = view?.CityName;
                x.District = view?.DistrictName;
                x.Town = view?.TownName;
            });
            return results;
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得指定区域TOP MIMO双流比小区列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("指定区域TOP MIMO双流比小区列表")]
        public IEnumerable<DoubleFlowView> Get(DateTime begin, DateTime end, int topCount, string orderSelection,
            string city, string district)
        {
            var results = _service.QueryTopDistrictViews(city, district, begin, end, topCount,
                orderSelection.GetEnumType<OrderDoubleFlowPolicy>());
            results.ForEach(x =>
            {
                var view = _eNodebQueryService.GetByENodebId(x.ENodebId);
                x.ENodebName = view?.Name;
                x.City = city;
                x.District = district;
                x.Town = view?.TownName;
            });
            return results;
        }
    }
}