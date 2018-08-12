using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("基站级流量查询控制器")]
    [ApiGroup("KPI")]
    public class ENodebFlowController : ApiController
    {
        private readonly ENodebFlowService _service;

        public ENodebFlowController(ENodebFlowService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的基站级流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("指定日期范围内的基站级流量统计")]
        public IEnumerable<ENodebFlowView> Get(DateTime begin, DateTime end, string frequency)
        {
            return _service.GetENodebFlowViews(begin, end, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的基站级流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("指定日期范围内的基站级流量统计")]
        public IEnumerable<ENodebFlowView> Get(DateTime begin, DateTime end, string city, string district, string frequency)
        {
            return _service.GetENodebFlowViews(begin, end, city, district, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的基站级流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("指定日期范围内的基站级流量统计")]
        public IEnumerable<ENodebFlowView> Get(DateTime begin, DateTime end, string city, string district, string town,
            string frequency)
        {
            return _service.GetENodebFlowViews(begin, end, city, district, town, frequency.GetBandFromFcn());
        }
    }
}