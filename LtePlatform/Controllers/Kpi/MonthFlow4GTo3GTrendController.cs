using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("月度4G感知优良率指标查询控制器")]
    [ApiGroup("KPI")]
    public class MonthFlow4GTo3GTrendController : ApiController
    {
        private readonly MonthKpiService _service;

        public MonthFlow4GTo3GTrendController(MonthKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("月度4G感知优良率指标查询")]
        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> Get()
        {
            return _service.QureyMonthFlow4GTo3GTrend();
        }
    }
}