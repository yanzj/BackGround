using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团小区信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationCellUpdateController : ApiController
    {
        private readonly ConstructionInformationService _service;

        public StationCellUpdateController(ConstructionInformationService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新小区基本信息")]
        [ApiParameterDoc("serialNumber", "需要更新的小区序列号")]
        [ApiParameterDoc("factoryDescription", "厂家，华为、中兴、贝尔、爱立信、诺基亚、大唐、烽火")]
        [ApiParameterDoc("duplexingDescription", "双工模式，FDD、TDD、FT双模")]
        [ApiParameterDoc("cellCoverageDescription", "小区覆盖类别，室外覆盖|室分覆盖|室外和室分同时覆盖")]
        [ApiParameterDoc("remoteTypeDescription", "天线拉远类别，天线和基站在一起的为“不拉远”，通过RRU拉远的填写\"拉远\"，部分天线拉远的填写\"部分天线拉远\"")]
        [ApiParameterDoc("antennaTypeDescription", "小区多天线类别，无天线|单天线|功分天线|RRU级联天线|含功分和级联天线|")]
        [ApiParameterDoc("coAntennaWithOtherCells", "是否与其它小区共天线，当小区中存在天线和其他小区共天线是填写是，否则填写否，如FDD/TDD共天线和 多频段小区共天线均填写是，否则填否")]
        [ApiResponse("更新是否成功")]
        public bool Update(string serialNumber, string factoryDescription, string duplexingDescription,
            string cellCoverageDescription, string remoteTypeDescription, string antennaTypeDescription,
            string coAntennaWithOtherCells)
        {
            return _service.Update(serialNumber, factoryDescription, duplexingDescription,
                cellCoverageDescription, remoteTypeDescription, antennaTypeDescription, coAntennaWithOtherCells);
        }

    }
}