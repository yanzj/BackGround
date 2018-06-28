using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团站址信息更新控制器")]
    public class StationInfoUpdateController : ApiController
    {
        private readonly StationInfoService _service;

        public StationInfoUpdateController(StationInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新站址位置信息，包括经纬度和地址")]
        [ApiParameterDoc("serialNum", "需要更新的站址编码")]
        [ApiParameterDoc("longtitute", "经度")]
        [ApiParameterDoc("lattitute", "纬度")]
        [ApiParameterDoc("address", "地址")]
        [ApiResponse("更新是否成功")]
        public bool UpdatePositionInfo(string serialNum, double longtitute, double lattitute, string address)
        {
            return _service.UpdateStationPosition(serialNum, longtitute, lattitute, address);
        }
    }
}