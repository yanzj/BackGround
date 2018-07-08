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
    [ApiControl("新增基站Excel信息查询控制器")]
    [ApiGroup("导入")]
    public class NewENodebExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly ENodebDumpService _dumpService;

        public NewENodebExcelsController(BasicImportService service, ENodebDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        [ApiDoc("查询所有Excel信息")]
        [ApiResponse("所有Excel信息")]
        public IEnumerable<ENodebExcel> Get()
        {
            return _service.GetNewENodebExcels();
        }

        [HttpPost]
        [ApiDoc("批量导入基站Excel信息")]
        [ApiParameterDoc("container", "待导入基站Excel信息")]
        [ApiResponse("成功导入数量")]
        public int Post(NewENodebListContainer container)
        {
            return _dumpService.DumpNewEnodebExcels(container.Infos);
        }
    }
}