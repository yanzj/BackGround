using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("新增CDMA基站Excel信息查询控制器")]
    public class NewBtsExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly BtsDumpService _dumpService;

        public NewBtsExcelsController(BasicImportService service, BtsDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        public IEnumerable<BtsExcel> Get()
        {
            return _service.GetNewBtsExcels();
        }

        [HttpPost]
        public int Post(NewBtsListContainer container)
        {
            return _dumpService.DumpBtsExcels(container.Infos);
        }
    }
}