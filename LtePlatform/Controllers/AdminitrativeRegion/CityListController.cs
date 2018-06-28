using Lte.Evaluations.DataService.Basic;
using Lte.Parameters.Entities;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("获取行政区域信息的控制器")]
    public class CityListController : ApiController
    {
        private readonly TownQueryService _service;

        public CityListController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得所有城市列表")]
        [ApiResponse("所有城市列表")]
        public IHttpActionResult Get()
        {
            var query = _service.GetCities();
            return query.Count == 0 ? (IHttpActionResult) BadRequest("Empty City List!") : Ok(query);
        }

        [HttpGet]
        [ApiDoc("获得指定城市下属的区域列表")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiResponse("区域列表")]
        public List<string> Get(string city)
        {
            return _service.GetDistricts(city);
        }

        [HttpGet]
        [ApiDoc("获得指定城市和区域下属的镇区列表")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("镇区列表")]
        public List<string> Get(string city, string district)
        {
            return _service.GetTowns(city, district);
        }
    }
}
