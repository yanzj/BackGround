using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("导入集团RRU控制器")]
    [ApiGroup("导入")]
    public class DumpStationRruController : ApiController
    {
        private readonly BasicImportService _service;

        public DumpStationRruController(BasicImportService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条信息")]
        [ApiResponse("导入结果")]
        public async Task<bool> Put()
        {
            return await _service.DumpOneStationRru();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的统计记录数")]
        [ApiResponse("当前服务器中待导入的统计记录数")]
        public int Get()
        {
            return _service.StationRruCount;
        }

    }
}