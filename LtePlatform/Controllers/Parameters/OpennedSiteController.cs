using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("已开通规划站点查询控制器")]
    public class OpennedSiteController : ApiController
    {
        private readonly PlanningQueryService _service;

        public OpennedSiteController(PlanningQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询区域内已开通的规划点")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("已开通规划点视图列表")]
        public IEnumerable<PlanningSiteView> Get(string city, string district)
        {
            return _service.GetENodebsByDistrict(city, district, true);
        }
    }
}