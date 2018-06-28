using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Dt;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("4G测试数据查询控制器")]
    public class Record4GController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public Record4GController(CsvFileInfoService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定数据文件的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiResponse("指定数据文件的测试数据")]
        public IEnumerable<FileRecord4G> Get(string fileName)
        {
            return _service.GetFileRecord4Gs(fileName.Replace("____", "+"));
        }

        [ApiDoc("查询指定数据文件和网格编号的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiParameterDoc("rasterNum", "网格编号")]
        [ApiResponse("指定数据文件的测试数据")]
        [HttpGet]
        public IEnumerable<FileRecord4G> Get(string fileName, int rasterNum)
        {
            return _service.GetFileRecord4Gs(fileName.Replace("____", "+"), rasterNum);
        }

        [HttpPost]
        [ApiDoc("给定数据文件名称和网格编号列表，查询覆盖指标列表")]
        [ApiParameterDoc("infoView", "包含数据文件名称和网格编号列表的视图")]
        [ApiResponse("覆盖指标列表")]
        public IEnumerable<FileRecordCoverage4G> Post(FileRasterInfoView infoView)
        {
            return _service.GetCoverage4Gs(infoView);
        }
    }
}