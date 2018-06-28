using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("后端投诉单变化趋势查询控制器")]
    public class ComplainTrendController : ApiController
    {
        private readonly ComplainService _service;

        public ComplainTrendController(ComplainService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询本月至当天为止投诉工单数量")]
        public async Task<int> GetCount(DateTime today)
        {
            return await _service.QueryThisMonthCount<ComplainService, ComplainItem>(today);
        }

        [HttpGet]
        public Tuple<IEnumerable<string>, IEnumerable<int>> GetTrend(DateTime date)
        {
            return _service.Query<ComplainService, ComplainItem>(date, x => x.BeginTime);
        }

        [HttpGet]
        public async Task<Tuple<List<string>, List<int>>> QueryCounts(DateTime countDate)
        {
            return await _service.QueryCounts<ComplainService, ComplainItem>(countDate);
        }

    }
}