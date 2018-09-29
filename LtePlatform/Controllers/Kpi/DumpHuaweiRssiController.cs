using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入华为RSSI控制器")]
    [ApiGroup("导入")]
    public class DumpHuaweiRssiController : ApiController
    {
        private readonly FlowService _service;

        public DumpHuaweiRssiController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条华为RSSI")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneHuaweiRssiStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的华为RSSI统计记录数")]
        [ApiResponse("当前服务器中待导入的华为RSSI统计记录数")]
        public int Get()
        {
            return _service.RssiHuaweiCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的华为RSSI统计记录")]
        public void Delete()
        {
            _service.ClearHuaweiRssiStats();
        }
    }
}