using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;

namespace LtePlatform.Controllers.Parameters
{
    public class DumpLteRruController : ApiController
    {
        private readonly CellDumpService _service;

        public DumpLteRruController(CellDumpService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<int> Put()
        {
            return await _service.ImportRru();
        }

        [HttpPost]
        public async Task<int> Post()
        {
            return await _service.UpdateCells();
        }

        [HttpGet]
        public int Get()
        {
            return BasicImportContainer.CellExcels.Count;
        }
    }
}