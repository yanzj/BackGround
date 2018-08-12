using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("政企客户需求查询控制器")]
    public class VipDemandController : ApiController
    {
        private readonly VipDemandService _service;

        public VipDemandController(VipDemandService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [HttpOptions]
        [ApiDoc("更新在线求助")]
        [ApiParameterDoc("dto", "在线求助信息")]
        public async Task<int> Post(VipDemandDto dto)
        {
            return await _service.UpdateAsync(dto);
        }

        [HttpGet]
        [ApiDoc("根据电话号码查询在线求助记录")]
        public List<VipDemandDto> Get(DateTime begin, DateTime end)
        {
            return _service.Query(begin, end);
        }

        [HttpGet]
        public List<VipDemandDto> GetByPhoneNumber(string phoneNumber)
        {
            return _service.Query(phoneNumber);
        }

        [HttpGet]
        public List<VipDemandDto> Get(string district, string town, DateTime begin, DateTime end)
        {
            return _service.Query(district, town, begin, end);
        }

        [HttpGet]
        public VipDemandDto Get(string serialNumber)
        {
            return _service.QuerySingle(serialNumber);
        }

        [HttpGet]
        public async Task<int> GetCount(DateTime today)
        {
            return await _service.QueryCount<VipDemandService, VipDemand>(today);
        }
    }
}