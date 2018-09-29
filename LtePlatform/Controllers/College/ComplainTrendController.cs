using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities.Complain;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("后端投诉单变化趋势查询控制器")]
    [ApiGroup("投诉")]
    public class ComplainTrendController : ApiController
    {
        private readonly ComplainService _service;
        private readonly OnlineSustainService _onlineSustainService;

        public ComplainTrendController(ComplainService service, OnlineSustainService onlineSustainService)
        {
            _service = service;
            _onlineSustainService = onlineSustainService;
        }

        [HttpGet]
        [ApiDoc("查询本月至当天为止投诉工单数量")]
        public async Task<int> GetCount(DateTime today)
        {
            return await _service.QueryThisMonthCount<ComplainService, ComplainItem>(today);
        }

        [HttpGet]
        [ApiDoc("查询上月1号至今每天的工单数量")]
        public Tuple<IEnumerable<string>, IEnumerable<int>> GetTrend(DateTime date)
        {
            return _service.Query<ComplainService, ComplainItem>(date, x => x.BeginTime);
        }

        [HttpGet]
        [ApiDoc("查询去年今日至今每月的工单数量")]
        public async Task<Tuple<List<string>, List<int>, List<int>>> QueryCounts(DateTime countDate)
        {
            return await _service.QueryCounts<ComplainService, ComplainItem, OnlineSustainService, OnlineSustain>(
                _onlineSustainService, countDate);
        }

    }
}