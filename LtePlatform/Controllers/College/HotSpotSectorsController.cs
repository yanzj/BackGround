using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询热点LTE扇区的控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class HotSpotSectorsController : ApiController
    {
        private readonly CollegeCellViewService _viewService;

        public HotSpotSectorsController(CollegeCellViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpGet]
        [ApiDoc("查询热点LTE小区")]
        [ApiParameterDoc("name", "热点名称")]
        [ApiResponse("热点LTE小区列表")]
        public IEnumerable<SectorView> Get(string name)
        {
            return _viewService.QueryHotSpotSectors(name);
        }
    }
}