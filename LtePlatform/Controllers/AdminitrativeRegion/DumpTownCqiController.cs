using Lte.Evaluations.DataService.RegionKpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("导入镇区CQI优良比查询控制器")]
    [ApiGroup("导入")]
    public class DumpTownCqiController : ApiController
    {
        private readonly TownKpiService _kpiService;

        public DumpTownCqiController(TownKpiService kpiService)
        {
            _kpiService = kpiService;
        }

        [HttpGet]
        public async Task<int[]> GetCqis(DateTime statDate)
        {
            return await _kpiService.GenerateTownCqis(statDate);
        }

    }
}