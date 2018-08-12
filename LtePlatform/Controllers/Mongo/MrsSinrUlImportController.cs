using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Lte.Evaluations.DataService.Mr;
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
    }
}