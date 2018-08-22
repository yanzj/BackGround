using LtePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Evaluations.DataService.Dump;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Maintainence;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("工单查询控制器")]
    [ApiGroup("KPI")]
    public class WorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有工单类型统计数据")]
        [ApiParameterDoc("chartType", "类型定义")]
        [ApiResponse("所有工单统计数据")]
        public IEnumerable<WorkItemChartTypeView> GetChartViews(string chartType)
        {
            return _service.QueryChartTypeViews(chartType);
        }
        
        [HttpGet]
        [ApiDoc("查询指定条件下工单视图列表")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiResponse("工单视图列表")]
        public IEnumerable<WorkItemView> Get(string statCondition, string typeCondition)
        {
            return _service.QueryViews(statCondition, typeCondition);
        }

        [HttpGet]
        [ApiDoc("查询指定条件下工单视图列表")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("工单视图列表")]
        public IEnumerable<WorkItemView> Get(string statCondition, string typeCondition, string district)
        {
            return _service.QueryViews(statCondition, typeCondition, district);
        }

        [HttpGet]
        [ApiDoc("查询对应基站编号的所有工单")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("对应的所有工单")]
        public async Task<IEnumerable<WorkItemView>> Get(int eNodebId)
        {
            return await _service.QueryViews(eNodebId);
        }

        [HttpGet]
        [ApiDoc("查询对应基站编号和扇区编号的所有工单")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("对应的所有工单")]
        public async Task<IEnumerable<WorkItemView>> Get(int eNodebId, byte sectorId)
        {
            return await _service.QueryViews(eNodebId, sectorId);
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiDoc("根据工单序列码查询对应的工单，这里假定工单的序列码是唯一的")]
        [ApiParameterDoc("serialNumber", "序列码")]
        [ApiResponse("对应的工单，这里假定工单的序列码是唯一的，若查不到，则返回空值")]
        public WorkItemView Get(string serialNumber)
        {
            return _service.Query(serialNumber);
        }

        [HttpGet]
        [ApiDoc("根据工单序列码查询对应的工单，并进行签收")]
        [ApiParameterDoc("signinNumber", "序列码")]
        [ApiResponse("对应的工单，这里假定工单的序列码是唯一的，若查不到或更新失败，则返回空值")]
        public async Task<WorkItemView> SignIn(string signinNumber)
        {
            return await _service.SignInWorkItem(signinNumber, User.Identity.Name);
        }

        [HttpGet]
        public async Task<WorkItemView> Finish(string finishNumber, string comments)
        {
            return await _service.FinishWorkItem(finishNumber, User.Identity.Name, comments);
        }

        [HttpPut]
        [AllowAnonymous]
        [ApiDoc("更新LTE扇区编号")]
        [ApiResponse("更新扇区编号数")]
        public int Put()
        {
            return _service.UpdateLteSectorIds();
        }

        [HttpPost]
        [ApiDoc("反馈工单信息")]
        [ApiParameterDoc("view", "反馈的工单信息，包括序列码和反馈内容")]
        [ApiResponse("反馈是否成功（写入数据库）")]
        public bool Post(WorkItemFeedbackView view)
        {
            return _service.FeedBack(User.Identity.Name, view.Message, view.SerialNumber);
        }
    }
}
