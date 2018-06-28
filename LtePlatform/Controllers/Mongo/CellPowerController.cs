using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;
using System.Web.Http;
using Lte.Parameters.Entities.Basic;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("查询小区功率参数控制器")]
    [ApiGroup("网管参数")]
    public class CellPowerController : ApiController
    {
        private readonly ICellPowerService _service;

        public CellPowerController(ICellPowerService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定小区的功率参数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("指定小区的功率参数，包括Pa/Pb和Rs功率参数")]
        public CellPower Get(int eNodebId, byte sectorId)
        {
            return _service.Query(eNodebId, sectorId);
        }
    }
}
