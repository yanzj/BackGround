using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    public class DistributionController : ApiController
    {
        private readonly ENodebQueryService _service;

        public DistributionController(ENodebQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<IndoorDistribution> Get(string district)
        {
            return _service.QueryDistributionSystems(district);
        }

        [HttpGet]
        [ApiDoc("获取经纬度范围内的室内分布列表")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的室内分布列表")]
        public IEnumerable<IndoorDistribution> Get(double west, double east, double south, double north)
        {
            return _service.QueryDistributionSystems(west, east, south, north);
        }
    }
}