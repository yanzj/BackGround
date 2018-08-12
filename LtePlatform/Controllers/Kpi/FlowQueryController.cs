using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("小区流量查询控制器")]
    [ApiGroup("KPI")]
    public class FlowQueryController : ApiController
    {
        private readonly FlowQueryService _service;

        public FlowQueryController(FlowQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区流量情况，按照日期排列")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("流量情况，按照日期排列")]
        public List<FlowView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.Query(eNodebId, sectorId, begin.Date, end.Date);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定小区平均流量情况")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("平均流量情况")]
        public FlowView GetAverage(int eNodebId, byte sectorId, DateTime beginDate, DateTime endDate)
        {
            return _service.QueryAverageView(eNodebId, sectorId, beginDate, endDate);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内镇区内小区流量情况，按照日期排列")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("流量情况，按照日期排列")]
        public IEnumerable<FlowView> Get(string city, string district, string town, DateTime beginDate, DateTime endDate,
            string frequency)
        {
            return _service.QueryAllTownViews(city, district, town, beginDate, endDate, frequency.GetBandFromFcn());
        }
    }
}