using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Evaluations.DataService.Dump;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("小区故障工单热点查询控制器")]
    [ApiGroup("维护")]
    public class CellAlarmWorkItemController : ApiController
    {
        private readonly AlarmWorkItemService _service;

        public CellAlarmWorkItemController(AlarmWorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询特定日期范围内的工单记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("count", "TOP个数")]
        [ApiResponse("特定日期范围内的工单记录")]
        public IEnumerable<CellAlarmWorkItemGroup> Get(DateTime begin, DateTime end, int count)
        {
            return _service.QueryCellAlarmGroups(begin, end, count);
        }
        
        [HttpGet]
        [ApiDoc("查询具有指定字符串的特定日期范围内的工单记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("text", "模糊查询字段")]
        [ApiResponse("特定日期范围内的工单记录")]
        public IEnumerable<CellAlarmWorkItemGroup> Get(DateTime begin, DateTime end, string text)
        {
            return _service.QueryCellAlarmGroups(begin, end, text);
        }

    }
}