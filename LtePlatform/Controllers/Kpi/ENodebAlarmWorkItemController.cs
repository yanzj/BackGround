﻿using System;
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
    [ApiControl("基站故障工单热点查询控制器")]
    [ApiGroup("维护")]
    public class ENodebAlarmWorkItemController : ApiController
    {
        private readonly AlarmWorkItemService _service;

        public ENodebAlarmWorkItemController(AlarmWorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询特定日期范围内的工单记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("count", "TOP个数")]
        [ApiResponse("特定日期范围内的工单记录")]
        public IEnumerable<ENodebAlarmWorkItemGroup> Get(DateTime begin, DateTime end, int count)
        {
            return _service.QueryENodebAlarmGroups(begin, end, count);
        }

    }
}