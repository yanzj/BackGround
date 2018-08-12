using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Evaluations.DataService.Dump;
using Lte.MySqlFramework.Entities;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("精确覆盖率类工单查询控制器")]
    [Authorize]
    public class PreciseWorkItemController : ApiController
    {
        private readonly PreciseWorkItemService _service;
        private readonly WorkItemService _workItemService;

        public PreciseWorkItemController(PreciseWorkItemService service, WorkItemService workItemService)
        {
            _service = service;
            _workItemService = workItemService;
        }

        [HttpGet]
        [ApiDoc("查询指定工单号码下的指定小区指标信息")]
        [ApiParameterDoc("number", "工单编号")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "小区编号")]
        [ApiResponse("指定小区指标信息")]
        public PreciseWorkItemCell Get(string number, int eNodebId, byte sectorId)
        {
            return _service.Query(number, eNodebId, sectorId);
        }

        [HttpGet]
        [ApiDoc("查询指定工单号码下的所有小区指标信息")]
        [ApiParameterDoc("number", "工单编号")]
        [ApiResponse("所有小区指标信息")]
        public List<PreciseWorkItemCell> Get(string number)
        {
            return _service.Query(number);
        }

        [HttpPost]
        [ApiDoc("创建精确覆盖率工单")]
        [ApiParameterDoc("view", "精确覆盖率的工单信息，包括小区信息、精确覆盖率信息、统计起止日期等")]
        [ApiResponse("创建是否成功，如成功返回工单编号，否则返回空值")]
        public async Task<string> Post(PreciseImportView view)
        {
            return await _workItemService.ConstructPreciseWorkItem(view.View, view.Begin, view.End, User.Identity.Name);
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiDoc("查询指定时间范围内的所有未完成的精确覆盖率工单")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("所有未完成的精确覆盖率工单")]
        public async Task<IEnumerable<WorkItemView>> Get(DateTime begin, DateTime end)
        {
            return await _workItemService.QueryPreciseViews(begin, end);
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiDoc("查询指定时间范围及指定区域内的所有未完成的精确覆盖率工单")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiResponse("所有未完成的精确覆盖率工单")]
        public async Task<IEnumerable<WorkItemView>> Get(DateTime begin, DateTime end, string district)
        {
            return await _workItemService.QueryPreciseViews(begin, end, district);
        }
    }

    [Authorize]
    [ApiControl("精确覆盖率工单干扰邻区查询控制器")]
    public class InterferenceNeighborWorkItemController : ApiController
    {
        private readonly PreciseWorkItemService _service;

        public InterferenceNeighborWorkItemController(PreciseWorkItemService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("保存干扰邻区信息")]
        [ApiParameterDoc("container", "干扰邻区信息容器")]
        public async Task Post(PreciseInterferenceNeighborsContainer container)
        {
            await _service.UpdateAsync(container);
        }
    }

    [Authorize]
    [ApiControl("精确覆盖率工单被干扰小区查询控制器")]
    public class InterferenceVictimWorkItemController : ApiController
    {
        private readonly PreciseWorkItemService _service;

        public InterferenceVictimWorkItemController(PreciseWorkItemService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("保存被干扰小区信息")]
        [ApiParameterDoc("container", "被干扰小区信息容器")]
        public async Task Post(PreciseInterferenceVictimsContainer container)
        {
            await _service.UpdateAsync(container);
        }
    }

    [Authorize]
    [ApiControl("工单内查询覆盖情况控制器")]
    public class CoverageWorkItemController : ApiController
    {
        private readonly PreciseWorkItemService _service;

        public CoverageWorkItemController(PreciseWorkItemService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("保存覆盖情况信息")]
        [ApiParameterDoc("container", "覆盖情况信息容器")]
        public async Task Post(PreciseCoveragesContainer container)
        {
            await _service.UpdateAsync(container);
        }
    }
}
