using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团天线信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationAntennaResetController : ApiController
    {
        private readonly StationAntennaService _service;

        public StationAntennaResetController(StationAntennaService service)
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
        [ApiDoc("重置指定天线序列码记录")]
        [ApiParameterDoc("serialNumber", "天线序列码")]
        [ApiResponse("重置是否成功")]
        public bool ResetBySerialNumber(string serialNumber)
        {
            return _service.ResetBySerialNumber(serialNumber);
        }

    }
}