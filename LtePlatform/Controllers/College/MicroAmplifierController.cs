using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LtePlatform.Controllers.College
{
    [ApiControl("手机伴侣查询控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class MicroAmplifierController : ApiController
    {
        private readonly MicroAmplifierService _service;

        public MicroAmplifierController(MicroAmplifierService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询全网手机伴侣分布")]
        [ApiResponse("全网手机伴侣分布")]
        public IEnumerable<MicroAmplifierView> Get()
        {
            return _service.QueryMicroAmplifierViews();
        }
    }
}
