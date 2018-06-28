using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("统计校园网名称控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeNamesController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeNamesController(CollegeStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有校园网名称")]
        [ApiResponse("所有校园网名称")]
        public IEnumerable<string> Get()
        {
            return _service.QueryNames();
        }

        [HttpGet]
        [ApiDoc("查询所有校园网名称")]
        [ApiParameterDoc("year", "指定年份")]
        [ApiResponse("所有校园网名称")]
        public IEnumerable<string> Get(int year)
        {
            return _service.QueryNames(year);
        }
    }
}