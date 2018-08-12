using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Station
{
    [AutoMapFrom(typeof(IndoorDistributionExcel))]
    [TypeDoc("集团网优平台室分信息")]
    public class IndoorDistribution : Entity, IGeoPoint<double>
    {
        [MemberDoc("室分编码")]
        public string IndoorSerialNum { get; set; }

        [MemberDoc("室分地址")]
        public string Address { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
        public string StationTown { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("小区标识")]
        public string CellSerialNum { get; set; }

        [MemberDoc("RRU标识")]
        public string RruSerialNum { get; set; }

        [MemberDoc("室分性质")]
        [AutoMapPropertyResolve("IndoorCategoryDescription", typeof(IndoorDistributionExcel), typeof(IndoorCategoryTransform))]
        public IndoorCategory IndoorCategory { get; set; }

        [MemberDoc("覆盖区域")]
        public string CoverageArea { get; set; }

        [MemberDoc("集成商")]
        public string Integritor { get; set; }

        [MemberDoc("分布系统性质")]
        [AutoMapPropertyResolve("IndoorNetworkDescription", typeof(IndoorDistributionExcel), typeof(IndoorNetworkTransform))]
        public IndoorNetwork IndoorNetwork { get; set; }

        [MemberDoc("是否合路")]
        [AutoMapPropertyResolve("HasCombiner", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsHasCombiner { get; set; }

        [MemberDoc("合路方式")]
        [AutoMapPropertyResolve("CombinerFunctionDescription", typeof(IndoorDistributionExcel), typeof(CombinerFunctionTransform))]
        public CombinerFunction CombinerFunction { get; set; }

        [MemberDoc("LTE是否合路老旧室分")]
        [AutoMapPropertyResolve("OldCombinerDescription", typeof(IndoorDistributionExcel), typeof(OldCombinerTransform))]
        public OldCombiner OldCombiner { get; set; }

        [MemberDoc("L网信源数量")]
        public byte LteSources { get; set; }

        [MemberDoc("合路集成商")]
        public string CombinerIntegrator { get; set; }

        [MemberDoc("级别")]
        [AutoMapPropertyResolve("DistributionClassDescription", typeof(IndoorDistributionExcel), typeof(ENodebClassTransform))]
        public ENodebClass DistributionClass { get; set; }

        [MemberDoc("巡检详细位置")]
        public string CheckingAddress { get; set; }

        [MemberDoc("是否与其他运营商合路")]
        [AutoMapPropertyResolve("CombinedWithOtherOperator", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsCombinedWithOtherOperator { get; set; }

        [MemberDoc("L网开通时间")]
        public DateTime? LteOpenDate { get; set; }

        [MemberDoc("单、双通道")]
        [AutoMapPropertyResolve("DistributionChannelDescription", typeof(IndoorDistributionExcel), typeof(DistributionChannelTransform))]
        public DistributionChannel DistributionChannel { get; set; }

        [MemberDoc("楼宇名称")]
        public string BuildingName { get; set; }

        [MemberDoc("楼宇编码")]
        public string BuildingCode { get; set; }

        [MemberDoc("楼宇地址")]
        public string BuildingAddress { get; set; }

        [MemberDoc("电梯数")]
        public byte? TotalLifts { get; set; }

        [MemberDoc("面积")]
        public double? BuildingArea { get; set; }

        [MemberDoc("层数")]
        public int TotalFloors { get; set; }

        [MemberDoc("楼内是否有地下停车场")]
        [AutoMapPropertyResolve("HasUnderGroundParker", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsHasUnderGroundParker { get; set; }

        [MemberDoc("覆盖面积")]
        public double? CoverageBuildingArea { get; set; }

        [MemberDoc("覆盖楼层数")]
        public int? CoverageFloors { get; set; }

        [MemberDoc("是否使用奇偶错层覆盖")]
        [AutoMapPropertyResolve("EvenOddFloorCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsEvenOddFloorCoverage { get; set; }

        [MemberDoc("LTE室分是否全覆盖")]
        [AutoMapPropertyResolve("LteFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsLteFullCoverage { get; set; }

        [MemberDoc("电梯是否LTE室分覆盖")]
        [AutoMapPropertyResolve("LiftLteFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsLiftLteFullCoverage { get; set; }

        [MemberDoc("地下室是否LTE室分覆盖")]
        [AutoMapPropertyResolve("UndergroundFullCoverage", typeof(IndoorDistributionExcel), typeof(YesToBoolTransform))]
        public bool IsUndergroundFullCoverage { get; set; }

        [MemberDoc("其它未覆盖区域描述")]
        public string OtherNoCoverageComments { get; set; }

        [MemberDoc("停车场覆盖情况")]
        public string ParkCoverages { get; set; }

        [MemberDoc("业主")]
        public string Yezhu { get; set; }

        [MemberDoc("电话")]
        public string YezhuPhone { get; set; }

        [MemberDoc("初次设计文件")]
        public string FirstDesignFile { get; set; }

        [MemberDoc("初次验收文件")]
        public string FirstYanshouFile { get; set; }

        [MemberDoc("改造设计文件")]
        public string ModifyDesignFile { get; set; }

        [MemberDoc("改造设验收")]
        public string ModifyYanshouFile { get; set; }

        [MemberDoc("维护人")]
        public string Maintainor { get; set; }

        [MemberDoc("维护人联系方式")]
        public string MaintainContact { get; set; }

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