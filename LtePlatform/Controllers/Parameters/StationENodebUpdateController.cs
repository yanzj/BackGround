using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团基站信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationENodebUpdateController : ApiController
    {
        private readonly ENodebBaseService _service;

        public StationENodebUpdateController(ENodebBaseService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新基站基本信息")]
        [ApiParameterDoc("eNodebId", "需要更新的基站编号")]
        [ApiParameterDoc("eNodebFactoryDescription", "厂家，华为、中兴、贝尔、爱立信、诺基亚、大唐、烽火")]
        [ApiParameterDoc("duplexingDescription", "双工模式，FDD、TDD、FT双模")]
        [ApiParameterDoc("totalCells", "小区数量，根据配置文件中所属该基站的小区计数")]
        [ApiParameterDoc("omcStateDescription", "omc中基站运行状态，在网运行、工程状态、长期退服")]
        [ApiParameterDoc("eNodebTypeDescription", "基站类型，BBU+RRU、微基站、微微基站、家庭基站")]
        [ApiResponse("更新是否成功")]
        public bool UpdateENodebBasicInfo(int eNodebId, string eNodebFactoryDescription, string duplexingDescription,
            int totalCells, string omcStateDescription, string eNodebTypeDescription)
        {
            return _service.UpdateENodebBasicInfo(eNodebId, eNodebFactoryDescription, duplexingDescription, totalCells,
                omcStateDescription, eNodebTypeDescription);
        }

        [HttpGet]
        [ApiDoc("更新基站位置信息，包括经纬度和地址")]
        [ApiParameterDoc("eNodebId", "需要更新的基站编号")]
        [ApiParameterDoc("longtitute", "经度")]
        [ApiParameterDoc("lattitute", "纬度")]
        [ApiParameterDoc("eNodebShared", "是否共享基站")]
        [ApiParameterDoc("omcIp", "OMCIP地址")]
        [ApiResponse("更新是否成功")]
        public bool UpdateENodebPositionInfo(int eNodebId, double longtitute, double lattitute, string eNodebShared,
            string omcIp)
        {
            return _service.UpdateENodebPositionInfo(eNodebId, longtitute, lattitute, eNodebShared, omcIp);
        }

    }
}