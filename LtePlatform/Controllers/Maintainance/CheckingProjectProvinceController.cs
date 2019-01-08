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
    [ApiControl("省公司巡检计划查询控制器")]
    [ApiGroup("维护")]
    public class CheckingProjectProvinceController : ApiController
    {
        private readonly CheckingProjectProvinceService _service;

        public CheckingProjectProvinceController(CheckingProjectProvinceService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("根据巡检内容查询巡检记录")]
        [ApiParameterDoc("contents", "巡检内容")]
        [ApiResponse("巡检记录")]
        public IEnumerable<CheckingProjectProvinceView> QueryByStationNumber(string contents)
        {
            return _service.QueryByContents(contents);
        }
        
        [HttpGet]
        [ApiDoc("根据日期时间范围查询巡检记录")]
        [ApiParameterDoc("begin", "开始时间")]
        [ApiParameterDoc("end", "结束时间")]
        [ApiResponse("巡检记录")]
        public IEnumerable<CheckingProjectProvinceView> QueryByBeginDate(DateTime begin, DateTime end)
        {
            return _service.QueryByBeginDate(begin, end);
        }
    }
}