using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("巡检信息查询控制器")]
    [ApiGroup("维护")]
    public class CheckingResultController : ApiController
    {
        private readonly CheckingService _service;

        public CheckingResultController(CheckingService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询经纬度范围内的记录")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的记录")]
        public IEnumerable<CheckingResultView> QueryRange(double west, double east, double south, double north)
        {
            return _service.QueryResultViews(west, east, south, north);
        }

    }
}