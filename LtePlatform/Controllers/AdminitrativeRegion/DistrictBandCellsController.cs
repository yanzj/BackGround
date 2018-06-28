using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("区域小区频段分布查询控制器")]
    public class DistrictBandCellsController : ApiController
    {
        private readonly TownQueryService _service;

        public DistrictBandCellsController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询区域小区频段分布")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("各区小区频段分布")]
        public List<DistrictBandClassStat> Get(string city)
        {
            return _service.QueryDistrictBandStats(city);
        }
    }
}