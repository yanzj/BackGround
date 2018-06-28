using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("抱怨量位置更新控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
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