using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("导入集团站址控制器")]
    [ApiGroup("导入")]
    public class DumpStationInfoController: ApiController
    {
        private readonly BasicImportService _service;

        public DumpStationInfoController(BasicImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的统计记录数")]
        [ApiResponse("当前服务器中待导入的统计记录数")]
        public int Get()
        {
            return _service.StationsCount;
        }

    }
}