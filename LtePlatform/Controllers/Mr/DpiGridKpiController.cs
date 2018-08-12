using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("含有DPI指标信息的栅格查询控制器")]
    public class DpiGridKpiController : ApiController
    {
        private readonly DpiGridKpiService _service;

        public DpiGridKpiController(DpiGridKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定栅格的DPI信息")]
        [ApiParameterDoc("x", "栅格横坐标")]
        [ApiParameterDoc("y", "栅格纵坐标")]
        public DpiGridKpiDto Get(int x, int y)
        {
            return _service.QueryKpi(x, y);
        }
    }
}