using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Mr;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("带有AGPS详细信息数据点查询控制器")]
    public class AgisDtPointsController : ApiController
    {
        private readonly NearestPciCellService _service;

        public AgisDtPointsController(NearestPciCellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询日期范围内带有AGPS详细信息数据点")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("日期范围内带有AGPS详细信息数据点")]
        public IEnumerable<AgisDtPoint> Get(DateTime begin, DateTime end)
        {
            return _service.QueryAgisDtPoints(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询日期范围内和指定主题带有AGPS详细信息数据点")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topic", "指定主题")]
        [ApiResponse("日期范围内和指定主题带有AGPS详细信息数据点")]
        public IEnumerable<AgisDtPoint> Get(DateTime begin, DateTime end, string topic)
        {
            return _service.QueryAgisDtPoints(begin, end, topic);
        }
    }
}
