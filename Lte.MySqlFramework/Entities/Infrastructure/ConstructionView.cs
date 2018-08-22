using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(ConstructionInformation))]
    public class ConstructionView : ILteCellQuery, IENodebName, ILteCellParameters<short?, int?>
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
        [AutoMapPropertyResolve("ENodebFactory", typeof(ConstructionInformation), typeof(ENodebFactoryDescriptionTransform))]
        public string ENodebFactoryDescription { get; set; }

        [MemberDoc("双工模式")]
        [AutoMapPropertyResolve("Duplexing", typeof(ConstructionInformation), typeof(DuplexingDescriptionTransform))]
        public string DuplexingDescription { get; set; }

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
        [AutoMapPropertyResolve("ENodebClass", typeof(ConstructionInformation), typeof(ENodebClassDescriptionTransform))]
        public string ENodebClassDescription { get; set; }

        [MemberDoc("小区覆盖类别")]
        [AutoMapPropertyResolve("CellCoverage", typeof(ConstructionInformation), typeof(CellCoverageDescriptionTransform))]
        public string CellCoverageDescription { get; set; }

        [MemberDoc("天线拉远类别")]
        [AutoMapPropertyResolve("RemoteType", typeof(ConstructionInformation), typeof(RemoteTypeDescriptionTransform))]
        public string RemoteTypeDescription { get; set; }

        [MemberDoc("小区多天线类别")]
        [AutoMapPropertyResolve("AntennaType", typeof(ConstructionInformation), typeof(AntennaTypeDescriptionTransform))]
        public string AntennaTypeDescription { get; set; }

        [MemberDoc("是否与其它小区共天线")]
        [AutoMapPropertyResolve("IsCoAntennaWithOtherCells", typeof(ConstructionInformation), typeof(YesNoTransform))]
        public string CoAntennaWithOtherCells { get; set; }

        [MemberDoc("天线编码")]
        public string AntennaSerial { get; set; }

        [MemberDoc("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [MemberDoc("是否CA小区")]
        [AutoMapPropertyResolve("IsCaCell", typeof(ConstructionInformation), typeof(YesNoTransform))]
        public string CaCell { get; set; }

        [MemberDoc("CA小区辅小区标识")]
        public string SecondaryCellId { get; set; }

        [MemberDoc("是否共享小区")]
        [AutoMapPropertyResolve("IsSharedCell", typeof(ConstructionInformation), typeof(YesNoTransform))]
        public string SharedCell { get; set; }

        [MemberDoc("业务类型")]
        public string NetworkTypeDescription { get; set; }
    }
}
