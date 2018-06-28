using System.Web.Http;
using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Entities.Channel;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("查询小区上行开环功率参数控制器")]
    [ApiGroup("网管参数")]
    public class UplinkOpenLoopPowerControlController : ApiController
    {
        private readonly UlOpenLoopPcService _service;

        public UplinkOpenLoopPowerControlController(UlOpenLoopPcService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定小区的开环功率控制参数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("指定小区的开环功率控制参数，包括PUSCH和PUCCH功率参数")]
        public CellOpenLoopPcView Get(int eNodebId, byte sectorId)
        {
            return _service.Query(eNodebId, sectorId);
        }
    }
}