using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("月度维护作业计划指标查询控制器")]
    [ApiGroup("KPI")]
    public class MonthMaintainProjectTrendController : ApiController
    {
        private readonly MonthKpiService _service;

        public MonthMaintainProjectTrendController(MonthKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("月度维护作业计划指标查询")]
        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> Get()
        {
            return _service.QueryMaintainProjectTrend();
        }
    }
}