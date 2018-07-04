using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团天线信息更新控制器")]
    [ApiGroup("基础信息")]
    public class StationAntennaUpdateController : ApiController
    {
        private readonly StationAntennaService _service;

        public StationAntennaUpdateController(StationAntennaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新RRU基本信息")]
        [ApiParameterDoc("rruNum", "需要更新的天线序列号")]
        [ApiParameterDoc("azimuth", "天线方位角(度)")]
        [ApiParameterDoc("eTilt", "预置下倾角(度)")]
        [ApiParameterDoc("mTilt", "机械下倾角(度)")]
        [ApiParameterDoc("antennaPorts", "天线端口数")]
        [ApiParameterDoc("antennaGain", "天线增益(dBi)")]
        [ApiParameterDoc("height", "天线总挂高(米)，填写从地面计算到天线的高度（落地塔填写天线至塔底的高度，楼顶塔填写天线至楼底的高度）")]
        [ApiResponse("更新是否成功")]
        public bool UpdateNumericInfo(string serialNumber, double azimuth, double eTilt, double mTilt,
            byte antennaPorts, string antennaGain, double height)
        {
            return _service.UpdateNumericInfo(serialNumber, azimuth, eTilt, mTilt, antennaPorts, antennaGain, height);
        }

        [HttpGet]
        [ApiDoc("更新RRU基本信息")]
        [ApiParameterDoc("rruNum", "需要更新的天线序列号")]
        [ApiParameterDoc("commonAntennaWithCdma", "是否与C网共用天线")]
        [ApiParameterDoc("antennaFactoryDescription", "天线厂家")]
        [ApiParameterDoc("antennaModel", "天线型号")]
        [ApiParameterDoc("integratedWithRru", "是否天线与RRU一体化")]
        [ApiParameterDoc("antennaBand", "天线频段，如 2100-2600MHz，支持多个频段时使用 \" / \"予以分隔")]
        [ApiParameterDoc("antennaDirectionDescription", "天线方向类型，全向|定向")]
        [ApiParameterDoc("antennaPolarDescription", "天线极化方式")]
        [ApiParameterDoc("electricAdjustable", "是否电调")]
        [ApiResponse("更新是否成功")]
        public bool UpdateBasicInfo(string serialNumber, string commonAntennaWithCdma, string antennaFactoryDescription,
            string antennaModel, string integratedWithRru, string antennaBand, string antennaDirectionDescription,
            string antennaPolarDescription, string electricAdjustable)
        {
            return _service.UpdateBasicInfo(serialNumber, commonAntennaWithCdma, antennaFactoryDescription,
                antennaModel, integratedWithRru, antennaBand, antennaDirectionDescription, antennaPolarDescription,
                electricAdjustable);
        }

        [HttpGet]
        [ApiDoc("更新RRU位置信息")]
        [ApiParameterDoc("rruNum", "需要更新的天线序列号")]
        [ApiParameterDoc("longtitute", "天线经度")]
        [ApiParameterDoc("lattitute", "天线纬度")]
        [ApiParameterDoc("antennaAddress", "天线地址，填写天线所在的楼面的详细地址，可以使用站址的详细地址+天线所处的楼层表示")]
        [ApiParameterDoc("antennaBeautyDescription", "美化天线类型")]
        [ApiParameterDoc("hasTowerAmplifier", "是否有塔放")]
        [ApiResponse("更新是否成功")]
        public bool UpdatePositionInfo(string serialNumber, double longtitute, double lattitute, string antennaAddress,
            string antennaBeautyDescription, string hasTowerAmplifier)
        {
            return _service.UpdatePositionInfo(serialNumber, longtitute, lattitute, antennaAddress,
                antennaBeautyDescription, hasTowerAmplifier);
        }

        [HttpGet]
        [ApiDoc("更新RRU共享信息")]
        [ApiParameterDoc("rruNum", "需要更新的天线序列号")]
        [ApiParameterDoc("coverageAreaDescription", "覆盖区域类型，密集市区|市区|县城|郊区|平原农村|水域农村|丘陵农村|山区农村|城中村")]
        [ApiParameterDoc("coverageRoadDescription", "覆盖道路类型，市区|县城|高铁|高速|铁路|地铁|航道|国道|省道|县道|乡道|无|水运")]
        [ApiParameterDoc("coverageHotspotDescription", "覆盖热点类型，校园|风景区|交通枢纽|集贸市场|会展中心|体育场|商业中心|大型企业|医院|港口|住宅小区|营业厅|星级宾馆|办公楼宇|公墓|海域|其它|无")]
        [ApiParameterDoc("boundaryTypeDescription", "边界类型，无|省际|省内|省内 / 省际|国际")]
        [ApiResponse("更新是否成功")]
        public bool UpdateCoverageInfo(string serialNumber, string coverageAreaDescription,
            string coverageRoadDescription,
            string coverageHotspotDescription, string boundaryTypeDescription)
        {
            return _service.UpdateCoverageInfo(serialNumber, coverageAreaDescription, coverageRoadDescription,
                coverageHotspotDescription, boundaryTypeDescription);
        }

    }
}