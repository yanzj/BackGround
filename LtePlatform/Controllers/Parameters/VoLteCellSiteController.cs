using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("室外800M小区站点查询控制器")]
    public class VoLteCellSiteController : ApiController
    {
        private readonly CellService _service;
        private readonly ENodebQueryService _eNodebQueryService;

        public VoLteCellSiteController(CellService service, ENodebQueryService eNodebQueryService)
        {
            _service = service;
            _eNodebQueryService = eNodebQueryService;
        }

        [HttpGet]
        [ApiDoc("分区查询室外小区站点")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("小区经纬度列表")]
        public IEnumerable<GeoPoint> Get(string city, string district)
        {
            return _service.QueryOutdoorCellSites(_eNodebQueryService.GetENodebsByDistrict(city, district), NetworkType.Volte);
        }
    }
}