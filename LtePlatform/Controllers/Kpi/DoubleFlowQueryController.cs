using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("小区MIMO双流比查询控制器")]
    [ApiGroup("KPI")]
    public class DoubleFlowQueryController : ApiController
    {
        private readonly DoubleFlowQueryService _service;

        public DoubleFlowQueryController(DoubleFlowQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区MIMO双流比情况，按照日期排列")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("MIMO双流比情况，按照日期排列")]
        public List<DoubleFlowView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.Query(eNodebId, sectorId, begin.Date, end.Date);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区平均MIMO双流比情况")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("平均MIMO双流比情况")]
        public DoubleFlowView GetAverage(int eNodebId, byte sectorId, DateTime beginDate, DateTime endDate)
        {
            return _service.QueryAverageView(eNodebId, sectorId, beginDate, endDate);
        }
    }
}