using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.Mr;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.MySqlFramework.Support.Container;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("导入MRS-SINRUL的控制器")]
    [ApiGroup("KPI")]
    public class MrsSinrUlImportController : ApiController
    {
        private readonly MrsSinrUlImportService _service;

        public MrsSinrUlImportController(MrsSinrUlImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("合并镇区指标")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("合并结果")]
        public IEnumerable<TownMrsSinrUl> GetMrs(DateTime statDate)
        {
            return _service.GetMergeMrsStats(statDate);
        }

        [HttpGet]
        [ApiDoc("获得指定日期范围内的已导入历史记录统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内的已导入历史记录统计")]
        public IEnumerable<SinrHistory> Get(DateTime begin, DateTime end)
        {
            return _service.GetSinrHistories(begin, end);
        }

        [HttpGet]
        [ApiDoc("获取TOP指标")]
        [ApiParameterDoc("topDate", "统计日期")]
        [ApiResponse("TOP指标条数")]
        public int GetTopMrs(DateTime topDate)
        {
            return _service.GetTopMrsSinrUls(topDate);
        }

        [HttpGet]
        [ApiDoc("获得等待导入数据库的记录总数")]
        [ApiResponse("等待导入数据库的记录总数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
        }

        [HttpPut]
        [ApiDoc("导入一条记录")]
        [ApiResponse("导入是否成功")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpDelete]
        [ApiDoc("清除等待导入数据库的记录")]
        public void Delete()
        {
            _service.ClearStats();
        }

        [HttpPost]
        [ApiDoc("导入镇区指标")]
        [ApiParameterDoc("container", "等待导入数据库的记录")]
        public async Task Post(TownSinrViewContainer container)
        {
            await _service.DumpTownStats(container);
        }
    }
}