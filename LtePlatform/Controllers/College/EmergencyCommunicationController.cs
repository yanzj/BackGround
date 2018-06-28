using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("应急通信需求查询控制器")]
    public class EmergencyCommunicationController : ApiController
    {
        private readonly EmergencyCommunicationService _service;

        public EmergencyCommunicationController(EmergencyCommunicationService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("导入一条应急通信需求记录")]
        [ApiParameterDoc("dto", "应急通信需求记录数据")]
        [ApiResponse("导入结果")]
        public int Post(EmergencyCommunicationDto dto)
        {
            return _service.Dump(dto);
        }

        [HttpGet]
        [ApiDoc("根据记录编号查询应急通信需求记录")]
        [ApiParameterDoc("id", "记录编号")]
        [ApiResponse("应急通信需求记录")]
        public EmergencyCommunicationDto Get(int id)
        {
            return _service.Query(id);
        }

        [HttpGet]
        [ApiDoc("查询一定时间范围内的应急通信需求记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("一定时间范围内的应急通信需求记录列表")]
        public List<EmergencyCommunicationDto> Get(DateTime begin, DateTime end)
        {
            return _service.Query(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询一定时间范围内指定镇区的应急通信需求记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("一定时间范围内制定镇区的应急通信需求记录列表")]
        public List<EmergencyCommunicationDto> Get(string district, string town, DateTime begin, DateTime end)
        {
            return _service.Query(district, town, begin, end);
        }
    }
}