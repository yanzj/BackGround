using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Abp.EntityFramework.Entities.College;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("统计校园网名称控制器")]
    [ApiGroup("专题优化")]
    public class CollegeNamesController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeNamesController(CollegeStatService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("根据名称查询校园网信息")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiResponse("校园网信息")]
        public CollegeView GetByName(string name)
        {
            return _service.QueryInfo(name);
        }
        
        [HttpGet]
        [ApiDoc("查询所有校园网名称")]
        public IEnumerable<string> GetAllNames()
        {
            return _service.QueryInfos().Select(x => x.Name);
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