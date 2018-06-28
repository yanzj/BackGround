using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("在用小区查询控制器")]
    public class CellInUseController : ApiController
    {
        private readonly CellService _service;
        private readonly CellRruService _rruService;

        public CellInUseController(CellService service, CellRruService rruService)
        {
            _service = service;
            _rruService = rruService;
        }

        [HttpGet]
        [ApiDoc("给定基站编号对定的扇区视图对象列表")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("对定的扇区视图对象列表")]
        public IEnumerable<SectorView> Get(int eNodebId)
        {
            return _service.QuerySectorsInUse(eNodebId);
        }

        [HttpGet]
        [ApiDoc("给定基站编号和扇区编号查询LTE小区，包含RRU信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("LTE小区，如果找不到则会返回错误，包含RRU信息")]
        public CellRruView Get(int eNodebId, byte sectorId)
        {
            return _rruService.GetCellRruView(eNodebId, sectorId);
        }

    }
}