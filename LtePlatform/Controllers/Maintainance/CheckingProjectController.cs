using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Evaluations.DataService.Maintainance;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Maintainance
{
    [ApiControl("巡检计划查询控制器")]
    [ApiGroup("维护")]
    public class CheckingProjectController : ApiController
    {
        private readonly CheckingProjectService _service;

        public CheckingProjectController(CheckingProjectService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("根据站点编码查询巡检记录")]
        [ApiParameterDoc("stationNumber", "站点编码")]
        [ApiResponse("巡检记录")]
        public IEnumerable<CheckingProjectView> QueryByStationNumber(string stationNumber)
        {
            return _service.QueryByStationNumber(stationNumber);
        }
        
        [HttpGet]
        [ApiDoc("根据日期时间范围查询巡检记录")]
        [ApiParameterDoc("begin", "开始时间")]
        [ApiParameterDoc("end", "结束时间")]
        [ApiResponse("巡检记录")]
        public IEnumerable<CheckingProjectView> QueryByBeginDate(DateTime begin, DateTime end)
        {
            return _service.QueryByBeginDate(begin, end);
        }
    }
}