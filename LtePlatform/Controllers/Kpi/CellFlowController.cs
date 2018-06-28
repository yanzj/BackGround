using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("小區级流量查询控制器")]
    [ApiGroup("KPI")]
    public class CellFlowController : ApiController
    {
        private readonly ENodebFlowService _service;

        public CellFlowController(ENodebFlowService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的室外小區级流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiParameterDoc("frequency", "频段描述")]
        [ApiResponse("指定日期范围内的室外小區级流量统计")]
        public IEnumerable<CellFlowView> Get(DateTime begin, DateTime end, string city, string district, string town,
            string frequency)
        {
            return _service.GetOutdoorCellFlowViews(begin, end, city, district, town, frequency.GetBandFromFcn());
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的室内小區级流量统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("指定日期范围内的室内小區级流量统计")]
        public IEnumerable<CellFlowView> Get(DateTime begin, DateTime end, string city, string district, string town)
        {
            return _service.GetIndoorCellFlowViews(begin, end, city, district, town);
        }
    }
}