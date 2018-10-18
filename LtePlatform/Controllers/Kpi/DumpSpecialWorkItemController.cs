using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("专业告警导入控制器")]
    [ApiGroup("导入")]
    public class DumpSpecialWorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public DumpSpecialWorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpPut]
        public bool Put()
        {
            return _service.DumpOneSpecial();
        }

        [HttpDelete]
        public void Delete()
        {
            _service.ClearDumpSpecials();
        }

        [HttpGet]
        public int Get()
        {
            return _service.QueryDumpSpecials();
        }
    }
}