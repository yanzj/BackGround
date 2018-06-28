using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("在用基站查询控制器")]
    public class ENodebInUseController : ApiController
    {
        private readonly ENodebQueryService _service;

        public ENodebInUseController(ENodebQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据行政区域条件查询在用基站列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("查询得到在用的基站列表结果")]
        public IEnumerable<ENodebView> Get(string city, string district, string town)
        {
            return _service.GetByTownNamesInUse(city, district, town);
        }

        [HttpGet]
        [ApiDoc("使用名称模糊查询，可以先后匹配基站名称、基站编号、规划编号和地址")]
        [ApiParameterDoc("name", "模糊查询的名称")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IEnumerable<ENodebView> Get(string name)
        {
            return _service.GetByGeneralNameInUse(name);
        }

        [HttpGet]
        [ApiDoc("根据行政区域条件查询在用基站簇，用於地圖顯示")]
        [ApiParameterDoc("cityName", "城市")]
        [ApiParameterDoc("districtName", "区域")]
        [ApiParameterDoc("townName", "镇区")]
        [ApiResponse("查询得到在用的基站簇结果")]
        public IEnumerable<ENodebCluster> GetCluster(string cityName, string districtName, string townName)
        {
            return _service.GetByTownNamesInUse(cityName, districtName, townName)
                .GroupBy(x => new
                {
                    LongtituteGrid = (int) (x.Longtitute * 100000),
                    LattituteGrid = (int) (x.Lattitute * 100000)
                }).Select(g => new ENodebCluster
                {
                    LongtituteGrid = g.Key.LongtituteGrid,
                    LattituteGrid = g.Key.LattituteGrid,
                    ENodebViews = g
                });
        }

    }
}