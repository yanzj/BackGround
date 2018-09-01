using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities.Infrastructure;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团站址信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationInfoResetController : ApiController
    {
        private readonly StationInfoService _service;

        public StationInfoResetController(StationInfoService service)
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
        [ApiDoc("按照站址编号重置站址信息")]
        [ApiParameterDoc("name", "站址名称")]
        [ApiResponse("重置是否成功")]
        public bool ResetBySerialNumber(string serialNumber)
        {
            return _service.ResetBySerialNumber(serialNumber);
        }
        
        [HttpGet]
        [ApiDoc("按照站址名称匹配重置站址信息")]
        [ApiParameterDoc("district", "区")]
        [ApiParameterDoc("name", "站址名称")]
        [ApiResponse("重置是否成功")]
        public bool ResetOneByStationDistrictAndName(string district, string stationName)
        {
            return _service.ResetOneByStationDistrictAndName(district, stationName);
        }
        
        [HttpGet]
        [ApiDoc("按照基站编号匹配重置站址信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("重置是否成功")]
        public bool ResetOneByStationName(string stationName)
        {
            return _service.ResetOneByStationName(stationName);
        }

    }
}