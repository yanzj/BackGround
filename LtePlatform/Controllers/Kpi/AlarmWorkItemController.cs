using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("故障工单查询控制器")]
    [ApiGroup("维护")]
    public class AlarmWorkItemController : ApiController
    {
        private readonly AlarmWorkItemService _service;

        public AlarmWorkItemController(AlarmWorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询特定日期范围内的工单记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("特定日期范围内的工单记录")]
        public IEnumerable<AlarmWorkItemView> Get(DateTime begin, DateTime end)
        {
            return _service.QueryByDateSpan(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询特定日期范围内的工单记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("networkType", "网络类型")]
        [ApiResponse("特定日期范围内的工单记录")]
        public IEnumerable<AlarmWorkItemView> Get(DateTime begin, DateTime end, string networkType)
        {
            return _service.QueryByDateSpan(begin, end, networkType);
        }
    }
}