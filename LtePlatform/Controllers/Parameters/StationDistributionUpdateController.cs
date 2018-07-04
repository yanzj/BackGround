using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团室分信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationDistributionUpdateController : ApiController
    {
        private readonly IndoorDistributionService _service;

        public StationDistributionUpdateController(IndoorDistributionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新RRU位置信息")]
        [ApiParameterDoc("serialNumber", "需要更新的室分序列号")]
        [ApiParameterDoc("address", "室分地址")]
        [ApiParameterDoc("longtitute", "经度")]
        [ApiParameterDoc("lattitute", "纬度")]
        [ApiParameterDoc("indoorCategoryDescription", "室分性质")]
        [ApiParameterDoc("checkingAddress", "巡检详细位置")]
        [ApiResponse("更新是否成功")]
        public bool UpdatePositionInfo(string serialNumber, string address, double longtitute, double lattitute,
            string indoorCategoryDescription, string checkingAddress)
        {
            return _service.UpdatePositionInfo(serialNumber, address, longtitute, lattitute, indoorCategoryDescription,
                checkingAddress);
        }

        [HttpGet]
        [ApiDoc("更新RRU系统信息")]
        [ApiParameterDoc("serialNumber", "需要更新的室分序列号")]
        [ApiParameterDoc("indoorNetworkDescription", "分布系统性质，C网专用,C + G + PC + G + W + P,C + P,C + P + W,C + W,G,P合路,C + W + G,C + G,C + L,C + W + L,C + W + G + L,C + G + L,L网专用,W网专用,L + W,其他")]
        [ApiParameterDoc("hasCombiner", "是否合路")]
        [ApiParameterDoc("combinerFunctionDescription", "合路方式，独立分布|系统独立|主干信源|合路|其他")]
        [ApiParameterDoc("oldCombinerDescription", "LTE是否合路老旧室分，合路P|合路C|合路W|合路P + W|合路C + W|合路P + C + W")]
        [ApiParameterDoc("combinedWithOtherOperator", "是否与其他运营商合路")]
        [ApiParameterDoc("distributionChannelDescription", "单、双通道，单通道|双通道")]
        [ApiResponse("更新是否成功")]
        public bool UpdateSystemInfo(string serialNumber, string indoorNetworkDescription, string hasCombiner,
            string combinerFunctionDescription, string oldCombinerDescription, string combinedWithOtherOperator,
            string distributionChannelDescription)
        {
            return _service.UpdateSystemInfo(serialNumber, indoorNetworkDescription, hasCombiner,
                combinerFunctionDescription, oldCombinerDescription, combinedWithOtherOperator,
                distributionChannelDescription);
        }

        [HttpGet]
        [ApiDoc("更新RRU位置信息")]
        [ApiParameterDoc("serialNumber", "需要更新的室分序列号")]
        [ApiParameterDoc("buildingName", "楼宇名称")]
        [ApiParameterDoc("buildingAddress", "楼宇地址")]
        [ApiParameterDoc("totalFloors", "层数")]
        [ApiParameterDoc("hasUnderGroundParker", "楼内是否有地下停车场")]
        [ApiParameterDoc("maintainor", "维护人")]
        [ApiResponse("更新是否成功")]
        public bool UpdateBuildingInfo(string serialNumber, string buildingName, string buildingAddress,
            int totalFloors, string hasUnderGroundParker, string maintainor)
        {
            return _service.UpdateBuildingInfo(serialNumber, buildingName, buildingAddress, totalFloors,
                hasUnderGroundParker, maintainor);
        }

        [HttpGet]
        [ApiDoc("更新RRU位置信息")]
        [ApiParameterDoc("serialNumber", "需要更新的室分序列号")]
        [ApiParameterDoc("evenOddFloorCoverage", "是否使用奇偶错层覆盖")]
        [ApiParameterDoc("lteFullCoverage", "LTE室分是否全覆盖")]
        [ApiParameterDoc("liftLteFullCoverage", "电梯是否LTE室分覆盖")]
        [ApiParameterDoc("undergroundFullCoverage", "地下室是否LTE室分覆盖")]
        [ApiResponse("更新是否成功")]
        public bool UpdateCoverageInfo(string serialNumber, string evenOddFloorCoverage, string lteFullCoverage,
            string liftLteFullCoverage, string undergroundFullCoverage)
        {
            return _service.UpdateCoverageInfo(serialNumber, evenOddFloorCoverage, lteFullCoverage, liftLteFullCoverage,
                undergroundFullCoverage);
        }

    }
}