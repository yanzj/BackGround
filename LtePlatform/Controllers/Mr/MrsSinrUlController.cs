using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("查询MRS SINR_UL小区指标控制器")]
    [ApiGroup("MR")]
    public class MrsSinrUlController : ApiController
    {
        private readonly MrsService _service;

        public MrsSinrUlController(MrsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询某一天MRS SINR_UL小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("某一天MRS SINR_UL小区指标")]
        public CellMrsSinrUlDto Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QuerySinrUlStat(eNodebId, sectorId, statDate);
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内MRS SINR_UL小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内MRS SINR_UL小区指标")]
        public IEnumerable<CellMrsSinrUlDto> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryMrsSinrUlStats(eNodebId, sectorId, begin, end);
        }
    }
}