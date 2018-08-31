using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("小区忙时PRB利用率查询控制器")]
    [ApiGroup("KPI")]
    public class HourPrbQueryController : ApiController
    {
        private readonly HourPrbQueryService _service;

        public HourPrbQueryController(HourPrbQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区忙时PRB利用率情况，按照日期排列")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("忙时PRB利用率情况，按照日期排列")]
        public List<HourPrbView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.Query(eNodebId, sectorId, begin.Date, end.Date);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区忙时平均PRB利用率情况")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("忙时平均PRB利用率情况")]
        public HourPrbView GetAverage(int eNodebId, byte sectorId, DateTime beginDate, DateTime endDate)
        {
            return _service.QueryAverageView(eNodebId, sectorId, beginDate, endDate);
        }
    }
}