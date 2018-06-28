using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("月度全量MR覆盖率指标查询控制器")]
    [ApiGroup("KPI")]
    public class MonthPreciseTrendController : ApiController
    {
        private readonly MonthKpiService _service;

        public MonthPreciseTrendController(MonthKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("月度全量MR覆盖率指标查询")]
        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> Get()
        {
            return _service.QureyMonthPreciseTrend();
        }
    }
}