using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities.Mr;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP SINRUL覆盖率查询控制器")]
    [ApiGroup("KPI")]
    public class TopMrsSinrUlController : ApiController
    {
        private readonly TopMrsSinrUlService _service;
        private readonly ENodebQueryService _eNodebQueryService;

        public TopMrsSinrUlController(TopMrsSinrUlService service, ENodebQueryService eNodebQueryService)
        {
            _service = service;
            _eNodebQueryService = eNodebQueryService;
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得TOP MR覆盖率TOP列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiResponse("TOP MR覆盖率TOP列表")]
        public IEnumerable<TopMrsSinrUlView> Get(DateTime begin, DateTime end, int topCount, string orderSelection)
        {
            return _service.GetAllTopViews(begin, end, topCount, orderSelection.GetEnumType<OrderMrsSinrUlPolicy>());
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得TOP MR覆盖率TOP列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("TOP MR覆盖率TOP列表")]
        public IEnumerable<TopMrsSinrUlView> Get(DateTime begin, DateTime end, int topCount, string orderSelection,
            string city, string district)
        {
            var eNodebs = _eNodebQueryService.GetENodebsByDistrict(city, district);
            return _service.GetPartialTopViews(begin, end, topCount, orderSelection.GetEnumType<OrderMrsSinrUlPolicy>(), eNodebs);
        }
    }
}