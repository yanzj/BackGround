using System;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class StationAntennaExcel : ICellAntenna<double>, IGeoPoint<double>
    {
        [ExcelColumn("天线编码")]
        public string AntennaNum { get; set; }

        [ExcelColumn("所属站址编码")]
        public string StationNum { get; set; }

        [ExcelColumn("所属站址名称")]
        public string StationName { get; set; }

        [ExcelColumn("站内天线编号")]
        public int LocalAntennaId { get; set; }

        [ExcelColumn("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [ExcelColumn("乡/镇/街道")]
        public string StationTown { get; set; }

        [ExcelColumn("划小单元")]
        public string SmallUnit { get; set; }

        [ExcelColumn("营服中心/营业部")]
        public string MarketCenter { get; set; }

        [ExcelColumn("片区")]
        public string StationRegion { get; set; }

        [ExcelColumn("簇")]
        public string StationCluster { get; set; }

        [ExcelColumn("网格")]
        public string Grid { get; set; }

        [ExcelColumn("天线方位角(度)")]
        public double Azimuth { get; set; }

        [ExcelColumn("预置下倾角(度)")]
        public double ETilt { get; set; }

        [ExcelColumn("电子下倾角_FDD2.1G(度)")]
        public double? ETilt2100 { get; set; }

        [ExcelColumn("RCU标识1_FDD2.1G")]
        public string FirstRcuFlag2100 { get; set; }

        [ExcelColumn("RCU标识2_FDD2.1G")]
        public string SecondRcuFlag2100 { get; set; }

        [ExcelColumn("电子下倾角_FDD1.8G(度)")]
        public double? ETilt1800 { get; set; }

        [ExcelColumn("RCU标识1_FDD1.8G")]
        public string FirstRcuFlag1800 { get; set; }

        [ExcelColumn("RCU标识2_FDD1.8G")]
        public string SecondRcuFlag1800 { get; set; }

        [ExcelColumn("电子下倾角_TDD2.6G(度)")]
        public double? ETilt2600 { get; set; }

        [ExcelColumn("RCU标识1_TDD2.6G")]
        public string FirstRcuFlag2600 { get; set; }

        [ExcelColumn("RCU标识2_TDD2.6G")]
        public string SecondRcuFlag2600 { get; set; }

        [ExcelColumn("电子下倾角_800M(度)")]
        public double? ETilt800 { get; set; }

        [ExcelColumn("RCU标识1_800M")]
        public string FirstRcuFlag800 { get; set; }

        [ExcelColumn("RCU标识2_800M")]
        public string SecondRcuFlag800 { get; set; }

        [ExcelColumn("机械下倾角(度)")]
        public double MTilt { get; set; }

        [ExcelColumn("是否与C网共用天线")]
        public string CommonAntennaWithCdma { get; set; }

        [ExcelColumn("天线厂家")]
        public string AntennaFactoryDescription { get; set; }

        [ExcelColumn("天线型号")]
        public string AntennaModel { get; set; }

        [ExcelColumn("是否天线与RRU一体化")]
        public string IntegratedWithRru { get; set; }

        [ExcelColumn("天线端口数")]
        public byte? AntennaPorts { get; set; }

        [ExcelColumn("天线频段")]
        public string AntennaBand { get; set; }

        [ExcelColumn("天线方向类型")]
        public string AntennaDirectionDescription { get; set; }

        [ExcelColumn("天线极化方式")]
        public string AntennaPolarDescription { get; set; }

        [ExcelColumn("是否电调")]
        public string ElectricAdjustable { get; set; }

        [ExcelColumn("天线增益(dBi)")]
        public string AntennaGain { get; set; }

        [ExcelColumn("天线水平半功率角(度) ")]
        public string HorizontalHalfDegree { get; set; }

        [ExcelColumn("天线垂直半功率角(度) ")]
        public string VerticalHalfDegree { get; set; }

        [ExcelColumn("上旁瓣抑制(dBc)")]
        public string UpperSidelobeInhibition { get; set; }

        [ExcelColumn("前后比(dB)")]
        public string FrontBackRatio { get; set; }

        [ExcelColumn("天线经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("天线纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("天线地址")]
        public string AntennaAddress { get; set; }

        [ExcelColumn("美化天线类型")]
        public string AntennaBeautyDescription { get; set; }
    
        [ExcelColumn("天线总挂高(米)")]
        public double Height { get; set; }

        [ExcelColumn("是否有塔放")]
        public string HasTowerAmplifier { get; set; }

        [ExcelColumn("馈线长度(米)")]
        public string FeederLength { get; set; }

        [ExcelColumn("馈线规格")]
        public string FeederModel { get; set; }

        [ExcelColumn("天线工程编码")]
        public string AntennaProjectNum { get; set; }

        [ExcelColumn("天线入网日期")]
        public DateTime? AntennaOpenDate { get; set; }

        [ExcelColumn("覆盖区域类型")]
        public string CoverageAreaDescription { get; set; }

        [ExcelColumn("覆盖道路类型")]
        public string CoverageRoadDescription { get; set; }

        [ExcelColumn("覆盖热点类型")]
        public string CoverageHotspotDescription { get; set; }

        [ExcelColumn("边界类型")]
        public string BoundaryTypeDescription { get; set; }

        [ExcelColumn("对端省份名称")]
        public string PeerProvince { get; set; }

        [ExcelColumn("对端地市名称")]
        public string PeerCity { get; set; }

        [ExcelColumn("对端国家名称")]
        public string PeerCountry { get; set; }

        [ExcelColumn("备注")]
        public string Comments { get; set; }

        [ExcelColumn("数据人工更新时间")]
        public DateTime? DataUpdateTime { get; set; }

        [ExcelColumn("数据更新人")]
        public string DataUpdater { get; set; }

        [ExcelColumn("自定义1")]
        public string Customize1 { get; set; }

        [ExcelColumn("自定义2")]
        public string Customize2 { get; set; }

        [ExcelColumn("自定义3")]
        public string Customize3 { get; set; }
    }
}
