using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("各区域测试日期信息的控制器")]
    [ApiGroup("测试")]
    public class AreaTestDateController : ApiController
    {
        private readonly AreaTestDateService _service;

        public AreaTestDateController(AreaTestDateService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得各区域测试日期信息，包括2G、3G、4G的最近一次测试日期")]
        [ApiResponse("各区域测试日期信息，包括2G、3G、4G的最近一次测试日期")]
        public IEnumerable<AreaTestDateView> Get()
        {
            return _service.QueryAllList();
        }

        [HttpGet]
        [ApiDoc("更新指定镇区的最近测试日期")]
        [ApiParameterDoc("testDate", "测试日期")]
        [ApiParameterDoc("networkType", "测试类型")]
        [ApiParameterDoc("townId", "镇区编号")]
        [ApiResponse("更新成功记录数")]
        public int Get(DateTime testDate, string networkType, int townId)
        {
            return _service.UpdateLastDate(testDate, networkType, townId);
        }
    }
}