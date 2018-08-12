using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Lte.Evaluations.DataService.College;

namespace LtePlatform.Controllers.College
{
    public class EmergencyFiberController : ApiController
    {
        private readonly EmergencyFiberService _service;

        [HttpGet]
        public IEnumerable<EmergencyFiberWorkItem> Get(int id)
        {
            return _service.Query(id);
        }

        public EmergencyFiberController(EmergencyFiberService service)
        {
            _service = service;
        }

        [HttpPost]
        public EmergencyFiberWorkItem Post(EmergencyFiberWorkItem item)
        {
            return _service.Create(item);
        }

        [HttpPut]
        public async Task<int> Put(EmergencyFiberWorkItem item)
        {
            return await _service.Finish(item);
        }
    }
}