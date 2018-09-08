using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.College;
using Lte.Evaluations.DataService.College;
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

    }
}