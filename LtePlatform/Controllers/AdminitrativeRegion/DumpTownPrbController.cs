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
    [ApiControl("导入镇区PRB利用率查询控制器")]
    [ApiGroup("导入")]
    public class DumpTownPrbController : ApiController
    {
        private readonly TownKpiService _kpiService;

        public DumpTownPrbController(TownKpiService kpiService)
        {
            _kpiService = kpiService;
        }

        [HttpGet]
        public async Task<int[]> GetPrbs(DateTime statDate)
        {
            return await _kpiService.GenerateTownPrbs(statDate);
        }

    }
}