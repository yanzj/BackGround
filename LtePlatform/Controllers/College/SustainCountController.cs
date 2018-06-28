using System;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("在线支撑次数查询控制器")]
    public class SustainCountController : ApiController
    {
        private readonly OnlineSustainService _service;

        public SustainCountController(OnlineSustainService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期的前一个月的在线支撑次数")]
        [ApiParameterDoc("today", "指定日期")]
        [ApiResponse("指定日期的前一个月的在线支撑次数")]
        public async Task<int> GetCount(DateTime today)
        {
            return await _service.QueryCount<OnlineSustainService, OnlineSustain>(today);
        }

    }
}