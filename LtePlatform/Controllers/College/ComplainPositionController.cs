using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities.Complain;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("抱怨量位置更新控制器")]
    [ApiGroup("投诉")]
    public class ComplainPositionController : ApiController
    {
        private readonly ComplainService _service;

        public ComplainPositionController(ComplainService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("抱怨信息位置相关信息查询")]
        [ApiParameterDoc("begin", "开始时间")]
        [ApiParameterDoc("end", "结束时间")]
        [ApiResponse("抱怨信息位置相关信息")]
        public IEnumerable<ComplainPositionDto> Get(DateTime begin, DateTime end)
        {
            return _service.QueryPositionDtos(begin, end);
        }

        [HttpPost]
        public async Task<int> Post(ComplainPositionDto dto)
        {
            return await _service.UpdateTown(dto);
        } 
    }
}