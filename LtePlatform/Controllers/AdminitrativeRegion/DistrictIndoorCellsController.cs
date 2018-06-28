using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("区域室内小区查询控制器")]
    public class DistrictIndoorCellsController : ApiController
    {
        private readonly TownQueryService _service;

        public DistrictIndoorCellsController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市内各区室内外小区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("各区室内外小区数量")]
        public List<DistrictIndoorStat> Get(string city)
        {
            return _service.QueryDistrictIndoorStats(city);
        } 
    }
}