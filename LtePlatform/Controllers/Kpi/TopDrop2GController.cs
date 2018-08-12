using Lte.Domain.Common;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.Policy;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Entities;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP掉话记录查询的控制器")]
    [ApiGroup("KPI")]
    public class TopDrop2GController : ApiController
    {
        private readonly TopDrop2GService _service;

        public TopDrop2GController(TopDrop2GService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("指定日期和城市，查询单个日期的TOP掉话记录，如果指定日期没有记录，则会匹配之前最近一天的记录")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("单个日期的TOP掉话记录，记录的日期可能比指定的日期要前")]
        public TopDrop2GDateView Get(DateTime statDate, string city)
        {
            return _service.GetDateView(statDate, city);
        }

        [HttpGet]
        [ApiDoc("指定日期区间和城市，查询该日期区间的TOP掉话记录集合")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("日期区间的TOP掉话记录集合")]
        public IEnumerable<TopDrop2GTrendView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.GetTrendViews(begin, end, city);
        }

        [HttpGet]
        [ApiDoc("指定日期区间、城市、排序标准以及TOP个数，查询该日期区间的TOP掉话记录集合")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("policy", "排序标准")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("日期区间的TOP掉话记录集合")]
        public IEnumerable<TopDrop2GTrendView> Get(DateTime begin, DateTime end, string city,
            string policy, int topCount)
        {
            return _service.GetTrendViews(begin, end, city).Order(policy.GetEnumType<OrderTopDrop2GPolicy>(), topCount);
        }
    }
}
