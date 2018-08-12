using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网基本查询控制器")]
    public class CollegeQueryController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeQueryController(CollegeStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据编号查询校园网信息")]
        [ApiParameterDoc("id", "校园编号")]
        [ApiResponse("校园网信息")]
        public IHttpActionResult Get(int id)
        {
            var info = _service.QueryInfo(id);
            return info == null ? (IHttpActionResult)BadRequest("College Id Not Found!") : Ok(info);
        }

        [HttpGet]
        [ApiDoc("根据名称和年份查询校园网信息")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiParameterDoc("year", "年份")]
        [ApiResponse("校园网信息")]
        public CollegeYearInfo Get(string name, int year)
        {
            return _service.QueryInfo(name, year);
        }

        [HttpGet]
        [ApiDoc("查询所有的校园网")]
        [ApiResponse("所有校园网信息")]
        public IEnumerable<CollegeInfo> Get()
        {
            return _service.QueryInfos();
        }

        [HttpPost]
        [ApiDoc("更新校园网基本信息")]
        [ApiParameterDoc("info", "校园网基本信息")]
        public async Task<int> Post(CollegeYearInfo info)
        {
            return await _service.SaveYearInfo(info);
        } 
    }
}
