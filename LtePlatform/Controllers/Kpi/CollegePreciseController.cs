using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("校园网精确覆盖率查询控制器")]
    public class CollegePreciseController : ApiController
    {
        private readonly CollegePreciseService _service;

        public CollegePreciseController(CollegePreciseService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定时间范围的指定校园的指标")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("collegeName", "指定校园")]
        public IEnumerable<CellPreciseKpiView> Get(string collegeName, DateTime begin, DateTime end)
        {
            return _service.GetViews(collegeName, begin, end);
        }
    }
}