using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入告警信息处理器")]
    [ApiGroup("导入")]
    public class DumpAlarmController : ApiController
    {
        private readonly AlarmsService _service;

        public DumpAlarmController(AlarmsService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条告警信息")]
        [ApiResponse("导入结果")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入告警数")]
        [ApiResponse("当前等待导入告警数")]
        public int Get()
        {
            return _service.GetAlarmsToBeDump();
        }

        [HttpGet]
        [ApiDoc("获得指定时间区间内的可以导入的告警统计列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("range", "导入数量")]
        [ApiResponse("可以导入的告警统计列表")]
        public IEnumerable<AlarmStat> Get(int begin, int range)
        {
            return _service.GetAlarmsToBeDump(begin, range);
        }

        [HttpGet]
        [ApiDoc("获取给定日期范围内的历史告警记录数")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("历史告警记录数")]
        public IEnumerable<AlarmHistory> Get(DateTime begin, DateTime end)
        {
            return _service.GetAlarmHistories(begin, end);
        }
        
        [HttpGet]
        [ApiDoc("获取给定日期范围内的历史MRO覆盖率记录数")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("历史MRO覆盖率记录数")]
        public IEnumerable<CoverageHistory> GetCoverage(DateTime beginDate, DateTime endDate)
        {
            return _service.GetCoverageHistories(beginDate, endDate);
        }

        [HttpDelete]
        [ApiDoc("清除已上传告警记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearAlarmStats();
        }
    }
}
