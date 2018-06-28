using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("VIP需求处理过程查询控制器")]
    public class VipProcessController : ApiController
    {
        private readonly VipDemandService _service;

        public VipProcessController(VipDemandService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据工单序列号查询VIP处理过程信息")]
        [ApiParameterDoc("serialNumber", "工单序列号")]
        [ApiResponse("VIP处理过程数据单元列表")]
        public IEnumerable<VipProcessDto> Get(string serialNumber)
        {
            return _service.QueryProcess(serialNumber);
        }
        
        [HttpPut]
        [ApiDoc("处理VIP需求处理过程，录入信息")]
        [ApiParameterDoc("dto", "单个VIP需求处理过程信息")]
        public async Task<int> Put(VipProcessDto dto)
        {
            return await _service.UpdateAsync(dto);
        }
    }
}