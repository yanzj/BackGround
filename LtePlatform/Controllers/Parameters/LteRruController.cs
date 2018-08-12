using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE RRU查询控制器")]
    public class LteRruController : ApiController
    {
        private readonly CellRruService _service;

        public LteRruController(CellRruService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据小区名称查询RRU信息")]
        [ApiParameterDoc("cellName", "小区名称")]
        [ApiResponse("RRU信息")]
        public LteRru Get(string cellName)
        {
            return _service.QueryRru(cellName);
        }
    }
}