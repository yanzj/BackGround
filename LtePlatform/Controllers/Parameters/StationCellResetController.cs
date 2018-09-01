using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团小区信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationCellResetController : ApiController
    {
        private readonly ConstructionInformationService _service;

        public StationCellResetController(ConstructionInformationService service)
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
        [ApiDoc("重置指定小区序列码记录")]
        [ApiParameterDoc("serialNumber", "小区序列码")]
        [ApiResponse("重置是否成功")]
        public bool ResetBySerialNumber(string serialNumber)
        {
            return _service.ResetBySerialNumber(serialNumber);
        }
        
        [HttpGet]
        [ApiDoc("根据小区标识重置小区记录")]
        [ApiParameterDoc("cellNum", "小区标识")]
        [ApiResponse("重置是否成功")]
        public bool ResetByCellNum(string cellNum)
        {
            return _service.ResetByCellNum(cellNum);
        }
        
        [HttpGet]
        [ApiDoc("根据小区名称重置小区记录")]
        [ApiParameterDoc("重置是否成功", "小区名称")]
        [ApiResponse("重置是否成功")]
        public bool ResetByCellName(string cellName)
        {
            return _service.ResetByCellName(cellName);
        }

    }
}