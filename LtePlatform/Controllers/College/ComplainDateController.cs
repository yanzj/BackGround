using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Support.View;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("日常抱怨量工单查询控制器")]
    [ApiGroup("投诉")]
    public class ComplainDateController : ApiController
    {
        private readonly ComplainService _service;

        public ComplainDateController(ComplainService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询最近一天抱怨量工单投诉情况")]
        [ApiParameterDoc("initialDate", "查询初始日期")]
        [ApiResponse("最近一天抱怨量工单投诉情况")]
        public DistrictComplainDateView Get(DateTime initialDate)
        {
            return _service.QueryLastDateStat(initialDate);
        }
        
        [HttpGet]
        [ApiDoc("查询日期范围内抱怨量工单投诉情况")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("抱怨量工单投诉情况列表")]
        public List<DistrictComplainView> Get(DateTime begin, DateTime end)
        {
            return _service.QueryDateSpanStats(begin.Date, end.Date);
        }
    }
}