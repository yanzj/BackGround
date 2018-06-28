using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities.Neighbor;
using System.Collections.Generic;
using System.Web.Http;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("系统邻区查询控制器")]
    public class NeighborCellMongoController : ApiController
    {
        private readonly NeighborCellMongoService _service;

        public NeighborCellMongoController(NeighborCellMongoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定小区的所有系统邻区列表")]
        [ApiParameterDoc("eNodebId", "小区的基站编号")]
        [ApiParameterDoc("sectorId", "小区扇区编号")]
        [ApiResponse("所有系统邻区列表")]
        public List<NeighborCellMongo> Get(int eNodebId, byte sectorId)
        {
            return _service.QueryNeighbors(eNodebId, sectorId);
        }

        [HttpGet]
        [ApiDoc("查询指定小区的所有反向系统邻区列表")]
        [ApiParameterDoc("destENodebId", "目标小区基站编号")]
        [ApiParameterDoc("destSectorId", "目标小区扇区编号")]
        [ApiResponse("所有反向系统邻区列表")]
        public List<NeighborCellMongo> GetReverse(int destENodebId, byte destSectorId)
        {
            return _service.QueryReverseNeighbors(destENodebId, destSectorId);
        }
    }
}
