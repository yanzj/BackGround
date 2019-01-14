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
using Lte.MySqlFramework.Support.Container;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("导入镇区精确覆盖率的控制器")]
    [ApiGroup("KPI")]
    public class TownPreciseImportController : ApiController
    {
        private readonly PreciseImportService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;
        private readonly PreciseStatService _statService;

        public TownPreciseImportController(PreciseImportService service, CollegeCellViewService collegeCellViewService,
            CollegeStatService collegeService, PreciseStatService statService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
            _statService = statService;
        }

        [ApiDoc("查询指定日期的精确覆盖率镇区统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiParameterDoc("frequency", "频点")]
        [ApiResponse("镇区精确覆盖率统计指标")]
        [HttpGet]
        public IEnumerable<TownPreciseView> Get(DateTime statTime, string frequency)
        {
            return _service.GetMergeStats(statTime, frequency.GetBandFromFcn());
        }
        
        [ApiDoc("查询指定日期的精确覆盖率校园统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiResponse("校园精确覆盖率统计指标")]
        [HttpGet]
        public IEnumerable<TownPreciseStat> Get(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            var stats = _statService.GetTimeSpanStats(beginDate, endDate);
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList
                    = (from c in cells
                        join s in stats on new {c.ENodebId, c.SectorId} equals new {ENodebId = s.CellId, s.SectorId}
                        select s).ToList();
                if (!viewListList.Any()) return null;
                var stat = viewListList.ArraySum().MapTo<TownPreciseStat>();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);
        }

        [HttpPost]
        [ApiDoc("导入镇区精确覆盖率")]
        [ApiParameterDoc("container", "等待导入数据库的记录")]
        public async Task Post(TownPreciseViewContainer container)
        {
            await _service.DumpTownStats(container);
        }
    }
}