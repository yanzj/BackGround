using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;

namespace LtePlatform.Controllers.Parameters
{
    public class DumpCdmaRruController : ApiController
    {
        private readonly CdmaCellDumpService _service;

        public DumpCdmaRruController(CdmaCellDumpService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<int> Put()
        {
            return await _service.ImportRru();
        }

        [HttpGet]
        public int Get()
        {
            return BasicImportContainer.CdmaCellExcels.Count;
        }
    }
}