using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("查询MRS RSRP小区指标控制器")]
    public class MrsRsrpController : ApiController
    {
        private readonly MrsService _service;

        public MrsRsrpController(MrsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询某一天MRS RSRP小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("某一天MRS RSRP小区指标")]
        public CellMrsRsrpDto Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QueryRsrpStat(eNodebId, sectorId, statDate);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内MRS RSRP小区指标")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内MRS RSRP小区指标")]
        public IEnumerable<CellMrsRsrpDto> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryRsrpStats(eNodebId, sectorId, begin, end);
        }
    }
}