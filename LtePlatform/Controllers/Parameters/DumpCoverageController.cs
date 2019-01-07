using System;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入三网覆盖小区信息处理器")]
    [ApiGroup("导入")]
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
        [ApiDoc("合并镇区指标")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiParameterDoc("frequency", "频点名称，如800、1800、2100")]
        [ApiResponse("合并结果")]
        public async Task<int> Get(DateTime statDate, string frequency)
        {
            return await _townService.GenerateTownStats(statDate, frequency.GetBandFromFcn());
        }
        
        [HttpDelete]
        [ApiDoc("清除已上传记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
