using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE基站查询控制器")]
    public class ENodebQueryController : ApiController
    {
        private readonly ENodebQueryService _service;

        public ENodebQueryController(ENodebQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据规划编号查询基站")]
        [ApiParameterDoc("planNum", "规划编号(FSL)")]
        [ApiResponse("基站视图")]
        public ENodebView Get(string planNum)
        {
            return _service.GetByPlanNum(planNum);
        }

        [HttpGet]
        [ApiDoc("查询行政上归属于某镇区统一区域的其他镇区，但地理位置位于本镇区范围内的LTE基站，用于后续调整镇区归属")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("满足以上条件的LTE基站视图列表")]
        public IEnumerable<ENodebView> Get(string city, string district, string town)
        {
            return _service.GetByTownArea(city, district, town);
        }

        [HttpGet]
        public ENodeb Get(int eNodebId, int townId)
        {
            return _service.UpdateTownInfo(eNodebId, townId);
        }

    }
}