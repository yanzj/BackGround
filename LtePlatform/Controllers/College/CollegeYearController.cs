using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网按年查询控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeYearController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeYearController(CollegeStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得指定年份的校园网信息列表")]
        [ApiParameterDoc("year", "指定年份")]
        [ApiResponse("校园网信息列表")]
        public IEnumerable<CollegeYearView> GetYearViews(int year)
        {
            return _service.QueryYearViews(year);
        }

    }
}