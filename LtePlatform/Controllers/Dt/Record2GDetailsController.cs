using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Dt;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("2G测试详细数据查询控制器")]
    [ApiGroup("测试")]
    public class Record2GDetailsController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public Record2GDetailsController(CsvFileInfoService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("给定数据文件名称和网格编号列表，查询覆盖指标列表")]
        [ApiParameterDoc("infoView", "包含数据文件名称和网格编号列表的视图")]
        [ApiResponse("覆盖指标列表")]
        public IEnumerable<FileRecord2G> Post(FileRasterInfoView infoView)
        {
            return _service.GetFileRecord2Gs(infoView);
        }
    }
}