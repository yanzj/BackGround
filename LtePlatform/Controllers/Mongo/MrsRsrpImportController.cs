using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("导入MRS-RSRP的控制器")]
    [ApiGroup("KPI")]
    public class MrsRsrpImportController : ApiController
    {
        private readonly MrsRsrpImportService _service;

        public MrsRsrpImportController(MrsRsrpImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("合并镇区指标")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("合并结果")]
        public IEnumerable<TownMrsRsrp> GetMrs(DateTime statDate, string band)
        {
            return _service.GetMergeMrsStats(statDate, band.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("获取TOP指标")]
        [ApiParameterDoc("topDate", "统计日期")]
        [ApiResponse("TOP指标条数")]
        public int GetTopMrs(DateTime topDate)
        {
            return _service.GetTopMrsRsrps(topDate);
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