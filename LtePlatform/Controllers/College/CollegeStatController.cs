using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Support;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("统计校园网信息控制器")]
    public class CollegeStatController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeStatController(CollegeStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据校园编号查询校园网统计信息")]
        [ApiParameterDoc("id", "校园编号")]
        [ApiParameterDoc("year", "年份")]
        [ApiResponse("校园网统计信息， 若查不到则会返回错误信息")]
        public IHttpActionResult Get(int id, int year)
        {
            var stat = _service.QueryStat(id, year);
            return stat == null ? (IHttpActionResult)BadRequest("ID Not Found!") : Ok(stat);
        }

        [HttpGet]
        [ApiDoc("查询所有校园网统计信息")]
        [ApiParameterDoc("year", "年份")]
        [ApiResponse("所有校园网统计信息")]
        public IEnumerable<CollegeStat> Get(int year)
        {
            return _service.QueryStats(year);
        }

        [Authorize]
        [HttpPost]
        public async Task<int> Post(CollegeInfo info)
        {
            return await _service.SaveCollegeInfo(info, User.Identity.Name.GetHashCode());
        } 
    }
}