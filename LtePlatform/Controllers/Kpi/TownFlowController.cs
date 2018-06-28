using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("镇级流量查询控制器")]
    [ApiGroup("KPI")]
    public class TownFlowController : ApiController
    {
        private readonly TownFlowService _service;

        public TownFlowController(TownFlowService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期前最近一天有记录的镇级天流量")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("镇级天流量，每个镇一条记录")]
        public IEnumerable<TownFlowView> Get(DateTime statDate, string frequency)
        {
            return _service.QueryLastDateView(statDate, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期前最近一天有记录的镇级天流量")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区名称")]
        [ApiResponse("镇级天流量，每个镇一条记录")]
        public IEnumerable<TownFlowView> Get(DateTime statDate, string city, string district, string frequency)
        {
            return _service.QueryLastDateView(statDate, city, district, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期有记录的镇级天流量")]
        [ApiParameterDoc("currentDate", "统计日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("镇级天流量，每个镇一条记录")]
        public IEnumerable<TownFlowStat> GetCurrentDate(DateTime currentDate, string frequency)
        {
            return _service.QueryCurrentDateStats(currentDate, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询时间段内区域天流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("天流量统计列表")]
        public IEnumerable<FlowRegionDateView> Get(DateTime begin, DateTime end, string city, string frequency)
        {
            return _service.QueryDateSpanStats(begin, end, city, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询时间段内镇天流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("天流量统计列表")]
        public IEnumerable<TownFlowView> Get(DateTime begin, DateTime end, string city, string district, string town)
        {
            return _service.QueryDateSpanViews(begin, end, city, district, town);
        }

        [HttpGet]
        [ApiDoc("查询时间段内镇天流量统计/按頻點匯總")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiParameterDoc("city", "城市名称")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("天流量统计列表")]
        public IEnumerable<TownFlowView> GetGroup(DateTime beginDate, DateTime endDate, string city, string district, string town)
        {
            return _service.QueryDateSpanGroupByFrequency(beginDate, endDate, city, district, town);
        }

        [HttpPost]
        public TownFlowStat Post(TownFlowStat stat)
        {
            return _service.Update(stat);
        }
    }
}