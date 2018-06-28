using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("工单导入控制器")]
    [ApiGroup("导入")]
    public class DumpWorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public DumpWorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<bool> Put()
        {
            return await _service.DumpOne();
        }

        [HttpDelete]
        public void Delete()
        {
            _service.ClearDumpItems();
        }

        [HttpGet]
        public int Get()
        {
            return _service.QueryDumpItems();
        }
    }
}