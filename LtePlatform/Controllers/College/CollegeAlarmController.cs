﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网告警查询控制器")]
    public class CollegeAlarmController : ApiController
    {
        private readonly CollegeAlarmService _service;

        public CollegeAlarmController(CollegeAlarmService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("查询单个校园的LTE基站视图列表，指定告警统计的时间段")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiParameterDoc("begin", "查询告警的开始日期")]
        [ApiParameterDoc("end", "查询告警的结束日期")]
        [ApiResponse("LTE告警列表")]
        public IEnumerable<Tuple<string, IEnumerable<AlarmStat>>> Get(string collegeName, DateTime begin, DateTime end)
        {
            return _service.QueryCollegeENodebAlarms(collegeName, begin, end);
        }
    }
}