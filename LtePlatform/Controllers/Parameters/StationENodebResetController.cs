using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团基站信息重置控制器")]
    [ApiGroup("基础信息")]
    public class StationENodebResetController : ApiController
    {
        private readonly ENodebBaseService _service;

        public StationENodebResetController(ENodebBaseService service)
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
        [ApiDoc("根据基站名称重置记录")]
        [ApiParameterDoc("eNodebName", "基站名称")]
        [ApiResponse("重置是否成功")]
        public bool ResetByENodebName(string eNodebName)
        {
            return _service.ResetByENodebName(eNodebName);
        }

    }
}