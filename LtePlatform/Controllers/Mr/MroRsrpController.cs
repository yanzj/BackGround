using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("查询MRO RSRP小区指标控制器")]
    public class MroRsrpController : ApiController
    {
        private readonly TopCoverageStatService _service;

        public MroRsrpController(TopCoverageStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询某一天MRO RSRP小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("某一天MRO RSRP小区指标")]
        public CoverageStatView Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.GetOneDayView(eNodebId, sectorId, statDate);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内MRO RSRP小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内MRO RSRP小区指标")]
        public IEnumerable<CoverageStatView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.GetDateSpanViews(eNodebId, sectorId, begin, end);
        }
    }
}