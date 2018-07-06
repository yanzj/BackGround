using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Dt;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("VoLTE测试数据查询控制器")]
    [ApiGroup("测试")]
    public class RecordVolteController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public RecordVolteController(CsvFileInfoService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定数据文件的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiResponse("指定数据文件的测试数据")]
        public IEnumerable<FileRecordVolte> Get(string fileName)
        {
            return _service.GetFileRecordVoltes(fileName.Replace("____", "+"));
        }

        [ApiDoc("查询指定数据文件和网格编号的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiParameterDoc("rasterNum", "网格编号")]
        [ApiResponse("指定数据文件的测试数据")]
        [HttpGet]
        public IEnumerable<FileRecordVolte> Get(string fileName, int rasterNum)
        {
            return _service.GetFileRecordVoltes(fileName.Replace("____", "+"), rasterNum);
        }

        [HttpPost]
        [ApiDoc("给定数据文件名称和网格编号列表，查询覆盖指标列表")]
        [ApiParameterDoc("infoView", "包含数据文件名称和网格编号列表的视图")]
        [ApiResponse("覆盖指标列表")]
        public IEnumerable<FileRecordCoverage4G> Post(FileRasterInfoView infoView)
        {
            return _service.GetCoverageVoltes(infoView);
        }
    }
}