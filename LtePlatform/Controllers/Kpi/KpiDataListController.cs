using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities.Cdma;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("传统指标查询控制器")]
    [ApiGroup("KPI")]
    public class KpiDataListController : ApiController
    {
        private readonly CdmaRegionStatService _service;

        public KpiDataListController(CdmaRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询不晚于指定日期的指定城市的单日指标")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("statDate", "指定日期")]
        [ApiResponse("单日分区指标和实际统计日期")]
        public async Task<CdmaRegionDateView> Get(string city, DateTime statDate)
        {
            return await _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定城市的详细指标")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("指定城市的详细指标")]
        public async Task<CdmaRegionStatDetails> Get(string city, DateTime beginDate, DateTime endDate)
        {
            return await _service.QueryStatDetails(beginDate, endDate, city);
        }

        [HttpGet]
        [ApiDoc("查询可用的指标名称")]
        [ApiResponse("可用的指标名称")]
        public List<string> Get()
        {
            return CdmaRegionStatDetails.KpiOptions;
        } 
    }
}
