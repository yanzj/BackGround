using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网LTE基站查询控制器")]
    [ApiGroup("专题优化")]
    public class CollegeENodebController : ApiController
    {
        private readonly CollegeENodebService _service;

        public CollegeENodebController(CollegeENodebService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询单个校园的LTE基站列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("LTE基站列表")]
        public IEnumerable<ENodebView> Get(string collegeName)
        {
            return _service.Query(collegeName);
        }

        [HttpPost]
        public async Task<int> Post(CollegeENodebIdsContainer container)
        {
            return await _service.UpdateENodebs(container);
        }
    }
}