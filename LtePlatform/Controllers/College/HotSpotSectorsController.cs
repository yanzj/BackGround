using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询热点LTE扇区的控制器")]
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