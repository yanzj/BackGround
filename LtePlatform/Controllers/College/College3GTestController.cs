using Lte.Evaluations.DataService.College;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网3G测试记录查询控制器")]
    public class College3GTestController : ApiController
    {
        private readonly College3GTestService _service;

        public College3GTestController(College3GTestService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期、时段和校园名称的3G测试记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiResponse("3G测试记录")]
        public IEnumerable<College3GTestView> GetViews(DateTime begin, DateTime end, string name)
        {
            var endDate = end.AddDays(1);
            return _service.GetViews(begin, endDate, name);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的各校园的平均速率")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("各校园的平均速率")]
        public Dictionary<string, double> Get(DateTime begin, DateTime end)
        {
            return _service.GetAverageRates(begin, end);
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内的各校园的单项指标")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("kpiName", "指标名称")]
        [ApiResponse("各校园的单项指标")]
        public Dictionary<string, double> Get(DateTime begin, DateTime end, string kpiName)
        {
            switch (kpiName)
            {
                case "users":
                    return _service.GetAverageUsers(begin, end);
                case "minRssi":
                    return _service.GetAverageMinRssi(begin, end);
                case "maxRssi":
                    return _service.GetAverageMaxRssi(begin, end);
                default:
                    return _service.GetAverageVswr(begin, end);
            }
        }

        [HttpPost]
        [ApiDoc("保存校园网3G测试记录")]
        [ApiParameterDoc("view", "校园网3G测试记录")]
        [ApiResponse("保存结果")]
        public async Task<int> Post(College3GTestView view)
        {
            return await _service.Post(view);
        }
    }
}
