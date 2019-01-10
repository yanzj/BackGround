using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("导入镇区双流比查询控制器")]
    [ApiGroup("导入")]
    public class DumpTownDoubleFlowController : ApiController
    {
        private readonly TownKpiService _kpiService;

        public DumpTownDoubleFlowController(TownKpiService kpiService)
        {
            _kpiService = kpiService;
        }

        [HttpGet]
        public async Task<int[]> GetPrbs(DateTime statDate)
        {
            return await _kpiService.GenerateTownDoubleFlows(statDate);
        }

    }
}