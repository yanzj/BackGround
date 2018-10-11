using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
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
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;

        public DumpCoverageController(CoverageStatService service, TownCoverageService townService, 
            CollegeCellViewService collegeCellViewService, CollegeStatService collegeService)
        {
            _service = service;
            _townService = townService;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
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
        
        [ApiDoc("查询指定日期的MRO覆盖率校园统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiResponse("校园MRO覆盖率统计指标")]
        [HttpGet]
        public async Task<int> Get(DateTime statTime)
        {
            var beginDate = statTime.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            var stats = _service.QueryStats(beginDate, endDate);
            var results = colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList
                    = (from c in cells
                        join s in stats on new {c.ENodebId, c.SectorId} equals new { s.ENodebId, s.SectorId}
                        select s).ToList();
                if (!viewListList.Any()) return null;
                var stat = viewListList.MapTo<List<TownCoverageStat>>().ArraySum();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                stat.Id = 0;
                return stat;
            }).Where(x => x != null).ToList();
            return await _townService.DumpTownStats(results);
        }

        [HttpDelete]
        [ApiDoc("清除已上传记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
