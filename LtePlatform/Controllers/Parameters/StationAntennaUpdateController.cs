using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团天线信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationAntennaUpdateController : ApiController
    {
        private readonly StationAntennaService _service;

        public StationAntennaUpdateController(StationAntennaService service)
        {
            _service = service;
        }

    }
}