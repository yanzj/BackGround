using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("栅格分簇查询控制器")]
    [ApiGroup("MR")]
    public class GridClusterController : ApiController
    {
        private readonly GridClusterService _service;

        public GridClusterController(GridClusterService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询某专题下的栅格分簇")]
        [ApiParameterDoc("theme", "专题名称")]
        [ApiResponse("栅格分簇列表")]
        public IEnumerable<GridClusterView> Get(string theme)
        {
            return _service.QueryClusterViews(theme);
        }

        [HttpGet]
        [ApiDoc("查询某专题下和地理范围内的栅格分簇")]
        [ApiParameterDoc("theme", "专题名称")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("栅格分簇列表")]
        public IEnumerable<GridClusterView> Get(string theme, double west, double east, double south, double north)
        {
            return _service.QueryClusterViews(theme, west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("查询地理范围内的栅格分簇")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("栅格分簇列表")]
        public IEnumerable<GridClusterView> Get(double west, double east, double south, double north)
        {
            return _service.QueryClusterViews(west, east, south, north);
        }

    }
}