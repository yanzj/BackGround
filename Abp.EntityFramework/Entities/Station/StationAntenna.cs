using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Station
{
    [AutoMapFrom(typeof(StationAntennaExcel))]
    [TypeDoc("集团网优平台天线信息")]
    public class StationAntenna : Entity, ICellAntenna<double>, IGeoPoint<double>
    {
        [MemberDoc("天线编码")]
        public string AntennaNum { get; set; }

        [MemberDoc("所属站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("所属站址名称")]
        public string StationName { get; set; }

        [MemberDoc("站内天线编号")]
        public int LocalAntennaId { get; set; }

        [MemberDoc("所属铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [MemberDoc("所属铁塔站址名称")]
        public string TowerStationName { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
        public string StationTown { get; set; }

        [MemberDoc("划小单元")]
        public string SmallUnit { get; set; }

        [MemberDoc("营服中心/营业部")]
        public string MarketCenter { get; set; }

        [MemberDoc("片区")]
        public string StationRegion { get; set; }

        [MemberDoc("簇")]
        public string StationCluster { get; set; }

        [MemberDoc("网格")]
        public string Grid { get; set; }

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
        [AutoMapPropertyResolve("CommonAntennaWithCdma", typeof(StationAntennaExcel), typeof(YesToBoolTransform))]
        public bool IsCommonAntennaWithCdma { get; set; }

        [MemberDoc("天线厂家")]
        [AutoMapPropertyResolve("AntennaFactoryDescription", typeof(StationAntennaExcel), typeof(AntennaFactoryTransform))]
        public AntennaFactory AntennaFactory { get; set; }

        [MemberDoc("天线型号")]
        public string AntennaModel { get; set; }

        [MemberDoc("是否天线与RRU一体化")]
        [AutoMapPropertyResolve("IntegratedWithRru", typeof(StationAntennaExcel), typeof(YesToBoolTransform))]
        public bool IsIntegratedWithRru { get; set; }

        [MemberDoc("天线端口数")]
        public byte? AntennaPorts { get; set; }

        [MemberDoc("天线频段")]
        public string AntennaBand { get; set; }

        [MemberDoc("天线方向类型")]
        [AutoMapPropertyResolve("AntennaDirectionDescription", typeof(StationAntennaExcel), typeof(AntennaDirectionTransform))]
        public AntennaDirection AntennaDirection { get; set; }

        [MemberDoc("天线极化方式")]
        [AutoMapPropertyResolve("AntennaPolarDescription", typeof(StationAntennaExcel), typeof(AntennaPolarTransform))]
        public AntennaPolar AntennaPolar { get; set; }

        [MemberDoc("是否电调")]
        [AutoMapPropertyResolve("ElectricAdjustable", typeof(StationAntennaExcel), typeof(YesToBoolTransform))]
        public bool IsElectricAdjustable { get; set; }

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
        [AutoMapPropertyResolve("AntennaBeautyDescription", typeof(StationAntennaExcel), typeof(AntennaBeautyTransform))]
        public AntennaBeauty AntennaBeauty { get; set; }

        [MemberDoc("天线总挂高(米)")]
        public double Height { get; set; }

        [MemberDoc("是否有塔放")]
        [AutoMapPropertyResolve("HasTowerAmplifier", typeof(StationAntennaExcel), typeof(YesToBoolTransform))]
        public bool IsHasTowerAmplifier { get; set; }

        [MemberDoc("馈线长度(米)")]
        public string FeederLength { get; set; }

        [MemberDoc("馈线规格")]
        public string FeederModel { get; set; }

        [MemberDoc("天线工程编码")]
        public string AntennaProjectNum { get; set; }

        [MemberDoc("天线入网日期")]
        public DateTime? AntennaOpenDate { get; set; }

        [MemberDoc("覆盖区域类型")]
        [AutoMapPropertyResolve("CoverageAreaDescription", typeof(StationAntennaExcel), typeof(CoverageAreaTransform))]
        public CoverageArea CoverageArea { get; set; }

        [MemberDoc("覆盖道路类型")]
        [AutoMapPropertyResolve("CoverageRoadDescription", typeof(StationAntennaExcel), typeof(CoverageRoadTransform))]
        public CoverageRoad CoverageRoad { get; set; }

        [MemberDoc("覆盖热点类型")]
        [AutoMapPropertyResolve("CoverageHotspotDescription", typeof(StationAntennaExcel), typeof(CoverageHotspotTransform))]
        public CoverageHotspot CoverageHotspot { get; set; }

        [MemberDoc("边界类型")]
        [AutoMapPropertyResolve("BoundaryTypeDescription", typeof(StationAntennaExcel), typeof(BoundaryTypeTransform))]
        public BoundaryType BoundaryType { get; set; }

        [MemberDoc("对端省份名称")]
        public string PeerProvince { get; set; }

        [MemberDoc("对端地市名称")]
        public string PeerCity { get; set; }

        [MemberDoc("对端国家名称")]
        public string PeerCountry { get; set; }

        [MemberDoc("备注")]
        public string Comments { get; set; }

        [MemberDoc("数据人工更新时间")]
        public DateTime? DataUpdateTime { get; set; }

        [MemberDoc("数据更新人")]
        public string DataUpdater { get; set; }

        [MemberDoc("自定义1")]
        public string Customize1 { get; set; }

        [MemberDoc("自定义2")]
        public string Customize2 { get; set; }

        [MemberDoc("自定义3")]
        public string Customize3 { get; set; }
    }
}
