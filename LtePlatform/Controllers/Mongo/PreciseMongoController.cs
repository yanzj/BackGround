using System;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiGroup("导入")]
    [ApiControl("从MongoDB导入精确覆盖率指标控制器")]
    public class PreciseMongoController : ApiController
    {
        private readonly PreciseImportService _service;

        public PreciseMongoController(PreciseImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("导入某一天的统计记录")]
        [ApiParameterDoc("statDate", "导入日期")]
        [ApiResponse("成功导入数量")]
        public int Get(DateTime statDate)
        {
            return _service.UpdateItems(statDate);
        }
    }
}