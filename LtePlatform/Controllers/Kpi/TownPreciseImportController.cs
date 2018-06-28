using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入镇区精确覆盖率的控制器")]
    [ApiGroup("KPI")]
    public class TownPreciseImportController : ApiController
    {
        private readonly PreciseImportService _service;

        public TownPreciseImportController(PreciseImportService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定日期的精确覆盖率镇区统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiResponse("镇区精确覆盖率统计指标")]
        [HttpGet]
        public IEnumerable<TownPreciseView> Get(DateTime statTime)
        {
            return _service.GetMergeStats(statTime);
        }

        [HttpGet]
        public IEnumerable<TownMrsRsrp> GetMrs(DateTime statDate)
        {
            return _service.GetMergeMrsStats(statDate);
        }

        [HttpGet]
        public IEnumerable<TopMrsRsrp> GetTopMrs(DateTime topDate)
        {
            return _service.GetTopMrsRsrps(topDate);
        }

        [HttpPost]
        [ApiDoc("导入镇区精确覆盖率")]
        [ApiParameterDoc("container", "等待导入数据库的记录")]
        public void Post(TownPreciseViewContainer container)
        {
            _service.DumpTownStats(container);
        }
    }
}