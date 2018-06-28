using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("镇区边界计算控制器")]
    public class TownBoundaryController : ApiController
    {
        private readonly TownBoundaryService _service;

        public TownBoundaryController(TownBoundaryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定镇区的边界坐标信息")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("镇区的边界坐标信息")]
        public IEnumerable<TownBoundaryView> Get(string city, string district, string town)
        {
            return _service.GetTownBoundaryViews(city, district, town);
        }

        [HttpGet]
        [ApiDoc("查询指定镇区的边界坐标信息")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区")]
        [ApiResponse("镇区的边界坐标信息")]
        public IEnumerable<TownBoundariesView> Get(string city, string district)
        {
            return _service.GetDistrictBoundaryViews(city, district);
        }

        [HttpGet]
        [ApiDoc("判断指定的经纬度是否在某个镇区内")]
        [ApiParameterDoc("longtitute", "经度")]
        [ApiParameterDoc("lattitute", "纬度")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("指定的经纬度是否在某个镇区内")]
        public bool Get(double longtitute, double lattitute, string city, string district, string town)
        {
            return _service.IsInTownBoundaries(longtitute, lattitute, city, district, town);
        }
    }
}