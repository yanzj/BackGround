using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网CDMA室内分布的控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeCdmaDistributionsController : ApiController
    {
        private readonly CollegeLteDistributionService _service;
        private readonly CollegeCdmaDistributionService _cdmaService;

        public CollegeCdmaDistributionsController(CollegeLteDistributionService service,
            CollegeCdmaDistributionService cdmaService)
        {
            _service = service;
            _cdmaService = cdmaService;
        }

        [HttpGet]
        [ApiDoc("查询校园网CDMA室内分布列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网CDMA室内分布列表")]
        public IEnumerable<IndoorDistribution> Get(string collegeName)
        {
            return _cdmaService.Query(collegeName);
        }
    }
}