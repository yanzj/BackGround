using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入华为CQI控制器")]
    [ApiGroup("导入")]
    public class DumpHuaweiCqiController : ApiController
    {
        private readonly FlowService _service;

        public DumpHuaweiCqiController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条华为CQI")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneHuaweiCqiStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的华为CQI统计记录数")]
        [ApiResponse("当前服务器中待导入的华为CQI统计记录数")]
        public int Get()
        {
            return _service.FlowHuaweiCqiCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的华为CQI统计记录")]
        public void Delete()
        {
            _service.ClearHuaweiCqiStats();
        }
    }
}