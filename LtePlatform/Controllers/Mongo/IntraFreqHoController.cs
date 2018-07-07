using Lte.Evaluations.DataService.Switch;
using System.Web.Http;
using Lte.Parameters.Entities.Switch;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("查询同频切换控制参数控制器")]
    [ApiGroup("网管参数")]
    public class IntraFreqHoController : ApiController
    {
        private readonly IntraFreqHoService _service;

        public IntraFreqHoController(IntraFreqHoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定基站的同频切换控制参数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("指定基站的同频切换控制参数")]
        public ENodebIntraFreqHoView Get(int eNodebId)
        {
            return _service.QueryENodebHo(eNodebId);
        }

        [HttpGet]
        [ApiDoc("查询指定小区的同频切换控制参数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("指定小区的同频切换控制参数")]
        public CellIntraFreqHoView Get(int eNodebId, byte sectorId)
        {
            return _service.QueryCellHo(eNodebId, sectorId);
        }
    }
}
