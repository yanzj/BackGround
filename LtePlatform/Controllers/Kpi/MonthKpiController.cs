using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("月度总体指标查询控制器")]
    [ApiGroup("KPI")]
    public class MonthKpiController: ApiController
    {
        private readonly MonthKpiService _service;

        public MonthKpiController(MonthKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("月度总体指标查询")]
        public IEnumerable<MonthKpiStat> Get()
        {
            return _service.QueryLastMonthKpiStats();
        }
    }
}