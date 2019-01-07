using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.College;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("高速查询控制器")]
    public class HighwayController : ApiController
    {
        private readonly HotSpotService _service;

        public HighwayController(HotSpotService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<HighwayView> Get()
        {
            return _service.QueryAllHighwayViews();
        }
    }
}