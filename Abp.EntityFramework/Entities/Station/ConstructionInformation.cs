using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Station
{
    [TypeDoc("集团网优平台小区信息")]
    [AutoMapFrom(typeof(ConstructionExcel))]
    public class ConstructionInformation : Entity, IIsInUse
    {
        [MemberDoc("小区标识")]
        public string CellSerialNum { get; set; }

        [MemberDoc("所属eNBID")]
        public int ENodebId { get; set; }

        [MemberDoc("所属eNBID名称")]
        public string ENodebName { get; set; }

        [MemberDoc("省/自治区/直辖市")]
        public string Province { get; set; }

        [MemberDoc("市/地区/州/盟")]
        public string City { get; set; }

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

        [MemberDoc("cellId")]
        public byte SectorId { get; set; }

        [MemberDoc("厂家内部cellId")]
        public byte LocalCellId { get; set; }

        [MemberDoc("小区名称")]
        public string CellName { get; set; }

        [MemberDoc("ECI")]
        public string Eci { get; set; }

        [MemberDoc("PCI")]
        public short Pci { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactoryDescription", typeof(ConstructionExcel), typeof(ENodebFactoryTransform))]
        public ENodebFactory ENodebFactory { get; set; }

        [MemberDoc("双工模式")]
        [AutoMapPropertyResolve("DuplexingDescription", typeof(ConstructionExcel), typeof(DuplexingTransform))]
        public Duplexing Duplexing { get; set; }

        [MemberDoc("频段标识")]
        public byte BandClass { get; set; }

        [MemberDoc("TAC")]
        public int? Tac { get; set; }

        [MemberDoc("ZC根序列索引")]
        public short? Prach { get; set; }

        [MemberDoc("上行中心频率(MHz)")]
        public double UplinkFrequencyMhz { get; set; }

        [MemberDoc("下行中心频率(MHz)")]
        public double FrequencyMhz { get; set; }

        [MemberDoc("上行中心频点号")]
        public int UplinkFrequency { get; set; }

        [MemberDoc("下行中心频点号")]
        public int Frequency { get; set; }

        [MemberDoc("上行带宽(MHz)")]
        public double UplinkBandwidth { get; set; }

        [MemberDoc("下行带宽(MHz)")]
        public double Bandwidth { get; set; }

        [MemberDoc("TD时隙比例")]
        public string TddConfig { get; set; }

        [MemberDoc("TD特殊时隙配比")]
        public string TddSpecialConfig { get; set; }

        [MemberDoc("小区等级")]
        [AutoMapPropertyResolve("ENodebClassDescription", typeof(ConstructionExcel), typeof(ENodebClassTransform))]
        public ENodebClass ENodebClass { get; set; }

        [MemberDoc("小区覆盖类别")]
        [AutoMapPropertyResolve("CellCoverageDescription", typeof(ConstructionExcel), typeof(CellCoverageTransform))]
        public CellCoverage CellCoverage { get; set; }

        [MemberDoc("天线拉远类别")]
        [AutoMapPropertyResolve("RemoteTypeDescription", typeof(ConstructionExcel), typeof(RemoteTypeTransform))]
        public RemoteType RemoteType { get; set; }

        [MemberDoc("小区多天线类别")]
        [AutoMapPropertyResolve("AntennaTypeDescription", typeof(ConstructionExcel), typeof(AntennaTypeTransform))]
        public AntennaType AntennaType { get; set; }

        [MemberDoc("是否与其它小区共天线")]
        [AutoMapPropertyResolve("CoAntennaWithOtherCells", typeof(ConstructionExcel), typeof(YesToBoolTransform))]
        public bool IsCoAntennaWithOtherCells { get; set; }

        [MemberDoc("天线编码")]
        public string AntennaSerial { get; set; }

        [MemberDoc("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [MemberDoc("是否CA小区")]
        [AutoMapPropertyResolve("CaCell", typeof(ConstructionExcel), typeof(YesToBoolTransform))]
        public bool IsCaCell { get; set; }

        [MemberDoc("CA小区辅小区标识")]
        public string SecondaryCellId { get; set; }

        [MemberDoc("是否共享小区")]
        [AutoMapPropertyResolve("SharedCell", typeof(ConstructionExcel), typeof(YesToBoolTransform))]
        public bool IsSharedCell { get; set; }

        [MemberDoc("业务类型")]
        [AutoMapPropertyResolve("NetworkTypeDescription", typeof(ConstructionExcel), typeof(NetworkTypeTransform))]
        public NetworkType NetworkType { get; set; }

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

        public bool IsInUse {get; set; }
    }
}