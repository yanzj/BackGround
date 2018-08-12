using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("镇区MR覆盖情况查询控制器")]
    public class TownMrGridController : ApiController
    {
        private readonly MrGridService _service;
        private readonly TownSupportService _supportService;

        public TownMrGridController(MrGridService service, TownSupportService supportService)
        {
            _service = service;
            _supportService = supportService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的MR覆盖率栅格信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiParameterDoc("town", "指定镇区")]
        [ApiResponse("指定区域最近日期内的MR覆盖率栅格信息列表")]
        public IEnumerable<MrCoverageGridView> Get(DateTime statDate, string district, string town)
        {
            var boundaries = _supportService.QueryTownBoundaries(district, town);
            if (boundaries == null) return new List<MrCoverageGridView>();
            return _service.QueryCoverageGridViews(statDate, boundaries, district);
        }

        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的栅格竞争信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiParameterDoc("town", "指定镇区")]
        [ApiParameterDoc("competeDescription", "竞争信息类型")]
        [ApiResponse("指定区域最近日期内的栅格竞争信息列表")]
        public IEnumerable<MrCompeteGridView> Get(DateTime statDate, string district, string town, string competeDescription)
        {
            var boundaries = _supportService.QueryTownBoundaries(district, town);
            if (boundaries == null) return new List<MrCompeteGridView>();
            var competeTuple =
                WirelessConstants.EnumDictionary["AlarmCategory"].FirstOrDefault(x => x.Item2 == competeDescription);
            var compete = (AlarmCategory?)competeTuple?.Item1;

            return _service.QueryCompeteGridViews(statDate, district, compete, boundaries);
        }
    }
}