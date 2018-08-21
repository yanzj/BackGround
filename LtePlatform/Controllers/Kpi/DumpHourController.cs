using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入忙时指标查询控制器")]
    [ApiGroup("导入")]
    public class DumpHourController : ApiController
    {
        private readonly HourKpiService _service;

        public DumpHourController(HourKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获取给定日期范围内的历史忙时指标记录数")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("历史忙时指标记录数")]
        public async Task<IEnumerable<HourKpiHistory>> Get(DateTime begin, DateTime end)
        {
            return await _service.GetHourHistories(begin.Date, end.Date);
        }

    }
}