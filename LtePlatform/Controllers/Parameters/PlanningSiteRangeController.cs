using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("地理规划站点查询控制器")]
    public class PlanningSiteRangeController : ApiController
    {
        private readonly PlanningQueryService _service;

        public PlanningSiteRangeController(PlanningQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询地理范围内的规划站点")]
        [ApiParameterDoc("west", "坐标西界")]
        [ApiParameterDoc("east", "坐标东界")]
        [ApiParameterDoc("south", "坐标南界")]
        [ApiParameterDoc("north", "坐标北界")]
        [ApiResponse("地理范围内的规划站点")]
        public IEnumerable<PlanningSiteView> Get(double west, double east, double south, double north)
        {
            return _service.QueryPlanningSiteViews(west, east, south, north);
        } 
    }
}