using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Wireless;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("区域、镇区在用基站扇区数量统计控制器")]
    public class RegionStatsController : ApiController
    {
        private readonly TownQueryService _service;

        public RegionStatsController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询分区域在用基站扇区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("分区域在用基站扇区数量")]
        public List<DistrictStat> Get(string city)
        {
            return _service.QueryDistrictStats(city);
        }

        [HttpGet]
        [ApiDoc("查询分镇区在用基站扇区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("分镇区在用基站扇区数量")]
        public List<TownStat> Get(string city, string district)
        {
            return _service.QueryTownStats(city, district);
        }

        [HttpGet]
        [ApiDoc("查询分镇区LTE扇区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("分镇区LTE扇区数量")]
        public LteCellStat Get(string city, string district, string town, bool isIndoor)
        {
            return _service.QueryLteCellStat(city, district, town, isIndoor);
        }
    }
}
