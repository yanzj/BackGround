using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Lte.Domain.Excel;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("分公司需求查询控制器")]
    public class BranchDemandController : ApiController
    {
        private readonly BranchDemandService _service;

        public BranchDemandController(BranchDemandService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询到指定日期当月发生的分公司需求数")]
        [ApiParameterDoc("today", "指定统计日期")]
        [ApiResponse("到指定日期当月发生的分公司需求数")]
        public async Task<int> GetCount(DateTime today)
        {
            return await _service.QueryCount<BranchDemandService, BranchDemand>(today);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的分公司需求列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("分公司需求列表")]
        public List<BranchDemandDto> Get(DateTime begin, DateTime end)
        {
            return _service.QueryList(begin, end);
        }

    }
}