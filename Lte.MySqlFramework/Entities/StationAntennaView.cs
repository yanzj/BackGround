using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(StationAntenna))]
    [TypeDoc("集团网优平台天线视图")]
    public class StationAntennaView : ICellAntenna<double>, IGeoPoint<double>
    {
        [MemberDoc("天线编码")]
        public string AntennaNum { get; set; }

        [MemberDoc("所属站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("所属站址名称")]
        public string StationName { get; set; }

        [MemberDoc("站内天线编号")]
        public int LocalAntennaId { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
        public string StationTown { get; set; }

        [MemberDoc("天线方位角(度)")]
        public double Azimuth { get; set; }

        [MemberDoc("预置下倾角(度)")]
        public double ETilt { get; set; }

        [MemberDoc("电子下倾角_FDD2.1G(度)")]
        public double? ETilt2100 { get; set; }

        [MemberDoc("RCU标识1_FDD2.1G")]
        public string FirstRcuFlag2100 { get; set; }

        [MemberDoc("RCU标识2_FDD2.1G")]
        public string SecondRcuFlag2100 { get; set; }

        [MemberDoc("电子下倾角_FDD1.8G(度)")]
        public double? ETilt1800 { get; set; }

        [MemberDoc("RCU标识1_FDD1.8G")]
        public string FirstRcuFlag1800 { get; set; }

        [MemberDoc("RCU标识2_FDD1.8G")]
        public string SecondRcuFlag1800 { get; set; }

        [MemberDoc("电子下倾角_TDD2.6G(度)")]
        public double? ETilt2600 { get; set; }

        [MemberDoc("RCU标识1_TDD2.6G")]
        public string FirstRcuFlag2600 { get; set; }

        [MemberDoc("RCU标识2_TDD2.6G")]
        public string SecondRcuFlag2600 { get; set; }

        [MemberDoc("电子下倾角_800M(度)")]
        public double? ETilt800 { get; set; }

        [MemberDoc("RCU标识1_800M")]
        public string FirstRcuFlag800 { get; set; }

        [MemberDoc("RCU标识2_800M")]
        public string SecondRcuFlag800 { get; set; }

        [MemberDoc("机械下倾角(度)")]
        public double MTilt { get; set; }

        [MemberDoc("是否与C网共用天线")]
        [AutoMapPropertyResolve("IsCommonAntennaWithCdma", typeof(StationAntenna), typeof(YesNoTransform))]
        public string CommonAntennaWithCdma { get; set; }

        [MemberDoc("天线厂家")]
        [AutoMapPropertyResolve("AntennaFactory", typeof(StationAntenna), typeof(AntennaFactoryDescriptionTransform))]
        public string AntennaFactoryDescription { get; set; }

        [MemberDoc("天线型号")]
        public string AntennaModel { get; set; }

        [MemberDoc("是否天线与RRU一体化")]
        [AutoMapPropertyResolve("IsIntegratedWithRru", typeof(StationAntenna), typeof(YesNoTransform))]
        public string IntegratedWithRru { get; set; }

        [MemberDoc("天线端口数")]
        public byte? AntennaPorts { get; set; }

        [MemberDoc("天线频段")]
        public string AntennaBand { get; set; }

        [MemberDoc("天线方向类型")]
        [AutoMapPropertyResolve("AntennaDirection", typeof(StationAntenna), typeof(AntennaDirectionDescriptionTransform))]
        public string AntennaDirectionDescription { get; set; }

        [MemberDoc("天线极化方式")]
        [AutoMapPropertyResolve("AntennaPolar", typeof(StationAntenna), typeof(AntennaPolarDescriptionTransform))]
        public string AntennaPolarDescription { get; set; }

        [MemberDoc("是否电调")]
        [AutoMapPropertyResolve("IsElectricAdjustable", typeof(StationAntenna), typeof(YesNoTransform))]
        public string ElectricAdjustable { get; set; }

        [MemberDoc("天线增益(dBi)")]
        public string AntennaGain { get; set; }

        [MemberDoc("天线水平半功率角(度) ")]
        public string HorizontalHalfDegree { get; set; }

        [MemberDoc("天线垂直半功率角(度) ")]
        public string VerticalHalfDegree { get; set; }

        [MemberDoc("上旁瓣抑制(dBc)")]
        public string UpperSidelobeInhibition { get; set; }

        [MemberDoc("前后比(dB)")]
        public string FrontBackRatio { get; set; }

        [MemberDoc("天线经度")]
        public double Longtitute { get; set; }

        [MemberDoc("天线纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("天线地址")]
        public string AntennaAddress { get; set; }

        [MemberDoc("美化天线类型")]
        [AutoMapPropertyResolve("AntennaBeauty", typeof(StationAntenna), typeof(AntennaBeautyDescriptionTransform))]
        public string AntennaBeautyDescription { get; set; }

        [MemberDoc("天线总挂高(米)")]
        public double Height { get; set; }

        [MemberDoc("是否有塔放")]
        [AutoMapPropertyResolve("IsHasTowerAmplifier", typeof(StationAntenna), typeof(YesNoTransform))]
        public string HasTowerAmplifier { get; set; }

        [MemberDoc("馈线长度(米)")]
        public string FeederLength { get; set; }

        [MemberDoc("馈线规格")]
        public string FeederModel { get; set; }

        [MemberDoc("天线工程编码")]
        public string AntennaProjectNum { get; set; }

        [MemberDoc("天线入网日期")]
        public DateTime? AntennaOpenDate { get; set; }

        [MemberDoc("覆盖区域类型")]
        [AutoMapPropertyResolve("CoverageArea", typeof(StationAntenna), typeof(CoverageAreaDescriptionTransform))]
        public string CoverageAreaDescription { get; set; }

        [MemberDoc("覆盖道路类型")]
        [AutoMapPropertyResolve("CoverageRoad", typeof(StationAntenna), typeof(CoverageRoadDescriptionTransform))]
        public string CoverageRoadDescription { get; set; }

        [MemberDoc("覆盖热点类型")]
        [AutoMapPropertyResolve("CoverageHotspot", typeof(StationAntenna), typeof(CoverageHotspotDescriptionTransform))]
        public string CoverageHotspotDescription { get; set; }

        [MemberDoc("边界类型")]
        [AutoMapPropertyResolve("BoundaryType", typeof(StationAntenna), typeof(BoundaryTypeDescriptionTransform))]
        public string BoundaryTypeDescription { get; set; }

        [MemberDoc("对端省份名称")]
        public string PeerProvince { get; set; }

        [MemberDoc("对端地市名称")]
        public string PeerCity { get; set; }

        [MemberDoc("对端国家名称")]
        public string PeerCountry { get; set; }

    }
}
