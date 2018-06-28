using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入中兴流量控制器")]
    [ApiGroup("导入")]
    public class DumpZteFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpZteFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条中兴流量")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneZteStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的中兴流量统计记录数")]
        [ApiResponse("当前服务器中待导入的中兴流量统计记录数")]
        public int Get()
        {
            return _service.FlowZteCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的中兴流量统计记录")]
        public void Delete()
        {
            _service.ClearZteStats();
        }
    }
}