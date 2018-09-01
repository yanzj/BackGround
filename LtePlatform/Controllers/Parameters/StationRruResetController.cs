using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团RRU信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationRruResetController : ApiController
    {
        private readonly StationRruService _service;

        public StationRruResetController(StationRruService service)
        {
            _service = service;
        }
        
        [HttpDelete]
        [ApiDoc("重置所有记录")]
        [ApiResponse("重置是否成功")]
        public bool ResetAll()
        {
            return _service.ResetAll();
        }
        
        [HttpGet]
        [ApiDoc("重置指定RRU序列码记录")]
        [ApiParameterDoc("serialNumber", "RRU序列码")]
        [ApiResponse("重置是否成功")]
        public bool ResetBySerialNumber(string serialNumber)
        {
            return _service.ResetBySerialNumber(serialNumber);
        }
        
        [HttpGet]
        [ApiDoc("根据基站编号和机框编号重置对应的记录")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("rackId", "机框编号")]
        [ApiResponse("重置是否成功")]
        public bool ResetByENodebIdAndRackId(int eNodebId, int rackId)
        {
            return _service.ResetByENodebIdAndRackId(eNodebId, rackId);
        }

    }
}