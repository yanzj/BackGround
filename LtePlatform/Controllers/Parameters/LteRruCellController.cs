using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE RRU 查询控制器")]
    public class LteRruCellController : ApiController
    {
        private readonly CellRruService _service;

        public LteRruCellController(CellRruService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据RRU名称查询小区列表")]
        [ApiParameterDoc("rruName", "RRU名称")]
        [ApiResponse("小区信息列表")]
        public IEnumerable<CellView> Get(string rruName)
        {
            return _service.QueryByRruName(rruName);
        } 
    }
}