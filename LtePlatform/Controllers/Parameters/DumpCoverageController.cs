using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入三网覆盖小区信息处理器")]
    public class DumpCoverageController : ApiController
    {
        private readonly CoverageStatService _service;
        private readonly TownCoverageService _townService;

        public DumpCoverageController(CoverageStatService service, TownCoverageService townService)
        {
            _service = service;
            _townService = townService;
        }


        [HttpPut]
        [ApiDoc("导入一条三网覆盖信息")]
        [ApiResponse("导入结果")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入三网覆盖小区数")]
        [ApiResponse("当前等待导入三网覆盖小区数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
        }

        [HttpGet]
        public int Get(DateTime statDate)
        {
            return _townService.GenerateTownStats(statDate);
        }

        [HttpDelete]
        [ApiDoc("清除已上传记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
