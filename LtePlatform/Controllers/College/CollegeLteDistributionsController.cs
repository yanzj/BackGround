using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Station;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网LTE室内分布的控制器")]
    [ApiGroup("专题优化")]
    public class CollegeLteDistributionsController : ApiController
    {
        private readonly CollegeLteDistributionService _service;

        public CollegeLteDistributionsController(CollegeLteDistributionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询校园网LTE室内分布列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网LTE室内分布列表")]
        public IEnumerable<IndoorDistribution> Get(string collegeName)
        {
            return _service.Query(collegeName);
        }
    }
}