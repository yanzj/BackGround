using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团室分信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationDistributionResetController : ApiController
    {
        private readonly IndoorDistributionService _service;

        public StationDistributionResetController(IndoorDistributionService service)
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
        [ApiDoc("重置指定室分序列码记录")]
        [ApiParameterDoc("serialNumber", "室分序列码")]
        [ApiResponse("重置是否成功")]
        public bool ResetBySerialNumber(string serialNumber)
        {
            return _service.ResetBySerialNumber(serialNumber);
        }
        
        [HttpGet]
        [ApiDoc("根据小区标识重置对应的记录")]
        [ApiParameterDoc("cellNum", "所属小区标识")]
        [ApiResponse("重置是否成功")]
        public bool ResetByCellNum(string cellNum)
        {
            return _service.ResetByCellNum(cellNum);
        }
        
        [HttpGet]
        [ApiDoc("根据RRU标识重置对应的记录")]
        [ApiParameterDoc("rruNum", "所属RRU标识")]
        [ApiResponse("重置是否成功")]
        public bool ResetByRruNum(string rruNum)
        {
            return _service.ResetByRruNum(rruNum);
        }

    }
}