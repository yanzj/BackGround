using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("小区站址查询控制器")]
    public class CellStationController : ApiController
    {
        private readonly CellRruService _service;

        public CellStationController(CellRruService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("根据RRU名称查询小区RRU")]
        [ApiParameterDoc("rruName", "RRU名称")]
        [ApiResponse("小区RRU信息列表")]
        public IEnumerable<CellRruView> GetByName(string rruName)
        {
            return _service.GetByRruName(rruName);
        }

        [HttpGet]
        [ApiDoc("根据规划编号查询小区RRU信息")]
        [ApiParameterDoc("planNum", "规划编号")]
        [ApiResponse("小区RRU信息列表")]
        public IEnumerable<CellRruView> GetViews(string planNum)
        {
            return _service.GetByPlanNum(planNum);
        }
    }
}