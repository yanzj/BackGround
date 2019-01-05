using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.MySqlFramework.Support.Container;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("导入MRS-TA的控制器")]
    [ApiGroup("导入")]
    public class MrsTadvImportController : ApiController
    {
        private readonly MrsTadvImportService _service;
        private readonly CollegeCellViewService _collegeCellViewService;
        private readonly CollegeStatService _collegeService;
        private readonly MrsService _statService;

        public MrsTadvImportController(MrsTadvImportService service, CollegeCellViewService collegeCellViewService,
            CollegeStatService collegeService, MrsService statService)
        {
            _service = service;
            _collegeCellViewService = collegeCellViewService;
            _collegeService = collegeService;
            _statService = statService;
        }

        [HttpGet]
        [ApiDoc("合并镇区指标")]
        [ApiParameterDoc("statTime", "统计日期")]
        [ApiParameterDoc("frequency", "频点名称，如800、1800、2100")]
        [ApiResponse("合并结果")]
        public IEnumerable<TownMrsTadv> GetMrs(DateTime statTime, string frequency)
        {
            return _service.GetMergeMrsStats(statTime, frequency.GetBandFromFcn());
        }
        
        [ApiDoc("查询指定日期的MRS-TA校园统计指标")]
        [ApiParameterDoc("statDate", "查询指定日期")]
        [ApiResponse("校园MRS覆盖率统计指标")]
        [HttpGet]
        public IEnumerable<TownMrsTadv> Get(DateTime statDate)
        {
            var beginDate = statDate.Date;
            var endDate = beginDate.AddDays(1);
            var colleges = _collegeService.QueryInfos();
            var stats = _statService.QueryDateSpanMrsTadvStats(beginDate, endDate);
            return colleges.Select(college =>
            {
                var cells = _collegeCellViewService.GetCollegeCells(college.Name);
                var viewListList
                    = (from c in cells
                        join s in stats on new {c.ENodebId, c.SectorId} equals new { s.ENodebId, s.SectorId}
                        select s).ToList();
                if (!viewListList.Any()) return null;
                var stat = viewListList.MapTo<List<TownMrsTadvDto>>().MapTo<List<TownMrsTadv>>().ArraySum();
                stat.FrequencyBandType = FrequencyBandType.College;
                stat.TownId = college.Id;
                return stat;
            }).Where(x => x != null);
        }
        
        [HttpGet]
        [ApiDoc("获得指定日期范围内的已导入历史记录统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内的已导入历史记录统计")]
        public IEnumerable<TadvHistory> Get(DateTime begin, DateTime end)
        {
            return _service.GetTadvHistories(begin, end);
        }

        [HttpGet]
        [ApiDoc("获取TOP指标")]
        [ApiParameterDoc("topDate", "统计日期")]
        [ApiResponse("TOP指标条数")]
        public int GetTopMrs(DateTime topDate)
        {
            return _service.GetTopMrsTadvs(topDate);
        }

        [HttpGet]
        [ApiDoc("获得等待导入数据库的记录总数")]
        [ApiResponse("等待导入数据库的记录总数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
        }

        [HttpPut]
        [ApiDoc("导入一条记录")]
        [ApiResponse("导入是否成功")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpDelete]
        [ApiDoc("清除等待导入数据库的记录")]
        public void Delete()
        {
            _service.ClearStats();
        }
        
        [HttpPost]
        [ApiDoc("导入镇区指标")]
        [ApiParameterDoc("container", "等待导入数据库的记录")]
        public async Task Post(TownTadvViewContainer container)
        {
            await _service.DumpTownStats(container);
        }
    }
}