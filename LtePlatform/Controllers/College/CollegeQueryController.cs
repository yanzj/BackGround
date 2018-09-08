using Lte.Evaluations.DataService.College;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities.College;
using Lte.MySqlFramework.Entities.College;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网基本查询控制器")]
    [ApiGroup("专题优化")]
    public class CollegeQueryController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeQueryController(CollegeStatService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("查询指定年份所有校园网信息")]
        [ApiParameterDoc("year", "指定年份")]
        [ApiResponse("所有校园网年度信息")]
        public IEnumerable<CollegeYearView> GetNamesByYear(int year)
        {
            return _service.QueryYearViews(year);
        }
        
        [HttpGet]
        [ApiDoc("根据编号查询校园网信息")]
        [ApiParameterDoc("id", "校园编号")]
        [ApiResponse("校园网信息")]
        public IEnumerable<CollegeYearView> GetById(int id)
        {
            return _service.QueryInfo(id);
        }

        [HttpGet]
        [ApiDoc("根据名称和年份查询校园网信息")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiParameterDoc("year", "年份")]
        [ApiResponse("校园网信息")]
        public CollegeYearView Get(string name, int year)
        {
            return _service.QueryInfo(name, year);
        }

        [HttpGet]
        [ApiDoc("查询所有的校园网")]
        [ApiResponse("所有校园网信息")]
        public IEnumerable<CollegeView> Get()
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
