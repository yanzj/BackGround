using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团RRU信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationRruUpdateController : ApiController
    {
        private readonly StationRruService _service;

        public StationRruUpdateController(StationRruService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新RRU基本信息")]
        [ApiParameterDoc("rruNum", "需要更新的RRU序列号")]
        [ApiParameterDoc("factoryDescription", "厂家，华为、中兴、贝尔、爱立信、诺基亚、大唐、烽火")]
        [ApiParameterDoc("duplexingDescription", "双工模式，FDD、TDD、FT双模")]
        [ApiParameterDoc("electricSourceDescription", "电源状态，直流|交流")]
        [ApiParameterDoc("antiThunder", "是否防雷")]
        [ApiParameterDoc("transmitPorts", "RRU的Tx端口数，指RRU的所有射频发端口数，包含占用和空闲的")]
        [ApiParameterDoc("receivePorts", "RRU的Rx端口数，指RRU的所有射频收端口数，包含占用和空闲的")]
        [ApiResponse("更新是否成功")]
        public bool UpdateBasicInfo(string rruNum, string factoryDescription, string duplexingDescription,
            string electricSourceDescription, string antiThunder, int transmitPorts, int receivePorts)

        {
            return _service.UpdateBasicInfo(rruNum, factoryDescription, duplexingDescription, electricSourceDescription,
                antiThunder, transmitPorts, receivePorts);
        }

        [HttpGet]
        [ApiDoc("更新RRU信源信息")]
        [ApiParameterDoc("rruNum", "需要更新的RRU序列号")]
        [ApiParameterDoc("classDescription", "RRU等级，系统根据对站址维护管理等级关联计算得出")]
        [ApiParameterDoc("indoorDistributionSerial", "室分编码，当是否信源RRU为“是”，此字段为必填")]
        [ApiParameterDoc("indoorSource", "是否室分信源RRU")]
        [ApiResponse("更新是否成功")]
        public bool UpdateSourceInfo(string rruNum, string classDescription, string indoorDistributionSerial,
            string indoorSource)
        {
            return _service.UpdateSourceInfo(rruNum, classDescription, indoorDistributionSerial, indoorSource);
        }

        [HttpGet]
        [ApiDoc("更新RRU位置信息")]
        [ApiParameterDoc("rruNum", "需要更新的RRU序列号")]
        [ApiParameterDoc("longtitute", "RRU经度")]
        [ApiParameterDoc("lattitute", "RRU纬度")]
        [ApiParameterDoc("address", "RRU地址，RRU所在地址的详细信息，室分、室外RRU通过站址地址关联")]
        [ApiParameterDoc("position", "描述RRU安装位置的详细信息。如：蓝天大厦10楼天面挂墙、国防大厦15楼弱电井")]
        [ApiResponse("更新是否成功")]
        public bool UpdatePositionInfo(string rruNum, double longtitute, double lattitute, string address,
            string position)
        {
            return _service.UpdatePositionInfo(rruNum, longtitute, lattitute, address, position);
        }

        [HttpGet]
        [ApiDoc("更新RRU共享信息")]
        [ApiParameterDoc("rruNum", "需要更新的小区RRU号")]
        [ApiParameterDoc("operatorUsageDescription", "共享属性，电信产权独有|联通产权电信共享|电信产权联通共享")]
        [ApiParameterDoc("shareFunctionDescription", "共享方式，共享载波|独立载波")]
        [ApiParameterDoc("virtualRru", "是否虚拟RRU")]
        [ApiResponse("更新是否成功")]
        public bool UpdateShareInfo(string rruNum, string operatorUsageDescription, string shareFunctionDescription,
            string virtualRru)
        {
            return _service.UpdateShareInfo(rruNum, operatorUsageDescription, shareFunctionDescription, virtualRru);
        }

    }
}