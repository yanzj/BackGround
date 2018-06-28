using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.ViewModels.RegionKpi;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("查询LTE基站的控制器")]
    public class ENodebController : ApiController
    {
        private readonly ENodebQueryService _service;

        public ENodebController(ENodebQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据行政区域条件查询基站列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IEnumerable<ENodebView> Get(string city, string district, string town)
        {
            return _service.GetByTownNames(city, district, town);
        }

        [HttpGet]
        public IEnumerable<ENodebView> Get(string city, string district)
        {
            return _service.GetByDistrictNames(city, district);
        }

        [HttpGet]
        [ApiDoc("使用名称模糊查询，可以先后匹配基站名称、基站编号、规划编号和地址")]
        [ApiParameterDoc("name", "模糊查询的名称")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IEnumerable<ENodebView> Get(string name)
        {
            return _service.GetByGeneralName(name);
        }

        [HttpGet]
        [ApiDoc("根据基站编号条件查询基站")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public ENodebView Get(int eNodebId)
        {
            return _service.GetByENodebId(eNodebId);
        }

        [HttpGet]
        [ApiDoc("获取经纬度范围内的基站视图列表")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的基站视图列表")]
        public IEnumerable<ENodebView> Get(double west, double east, double south, double north)
        {
            return _service.QueryENodebViews(west, east, south, north);
        }

        [HttpPost]
        [ApiDoc("获取经纬度范围内的除某些基站外的基站视图列表")]
        [ApiParameterDoc("container", "指定条件范围")]
        [ApiResponse("指定条件范围内的基站视图列表")]
        public IEnumerable<ENodebView> Post(ENodebRangeContainer container)
        {
            return _service.QueryENodebViews(container);
        }
    }
}
