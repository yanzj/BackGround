using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入忙时PRB指标控制器")]
    [ApiGroup("导入")]
    public class DumpHourPrbController : ApiController
    {
        private readonly HourKpiService _service;

        public DumpHourPrbController(HourKpiService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条PRB指标")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOnePrbStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的PRB指标统计记录数")]
        [ApiResponse("当前服务器中待导入的PRB指标统计记录数")]
        public int Get()
        {
            return _service.HourPrbCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的PRB指标统计记录")]
        public void Delete()
        {
            _service.ClearPrbStats();
        }
    }
}