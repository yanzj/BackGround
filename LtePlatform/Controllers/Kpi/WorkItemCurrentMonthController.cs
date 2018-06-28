using System;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("本月（上个26日至下个25日）工单完成情况查询控制器")]
    [ApiGroup("KPI")]
    public class WorkItemCurrentMonthController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemCurrentMonthController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询本月（工单要求完成时间介于上个26日至下个25日）工单完成情况")]
        [ApiResponse("本月（上个26日至下个25日）工单完成情况, 包括总数、已完成数和超时数")]
        public async Task<Tuple<int, int, int>> Get()
        {
            return await _service.QueryTotalItemsThisMonth();
        }
    }
}