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
    [ApiControl("导入KPI查询控制器")]
    [ApiGroup("导入")]
    public class DumpStatsController : ApiController
    {
        private readonly TownKpiService _kpiService;

        public DumpStatsController(TownKpiService kpiService)
        {
            _kpiService = kpiService;
        }
        
        [HttpGet]
        public async Task<int[]> Get(DateTime statDate)
        {
            return await _kpiService.GenerateTownStats(statDate);
        }
    }
}