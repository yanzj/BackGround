using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Maintainence
{
    [AutoMapFrom(typeof(IndoorDistribution))]
    [TypeDoc("集团网优平台室分信息")]
    public class IndoorDistributionView : IGeoPoint<double>
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
        [AutoMapPropertyResolve("IndoorCategory", typeof(IndoorDistribution), typeof(IndoorCategoryDescriptionTransform))]
        public string IndoorCategoryDescription { get; set; }

        [MemberDoc("覆盖区域")]
        public string CoverageArea { get; set; }

        [MemberDoc("集成商")]
        public string Integritor { get; set; }

        [MemberDoc("分布系统性质")]
        [AutoMapPropertyResolve("IndoorNetwork", typeof(IndoorDistribution), typeof(IndoorNetworkDescriptionTransform))]
        public string IndoorNetworkDescription { get; set; }

        [MemberDoc("是否合路")]
        [AutoMapPropertyResolve("IsHasCombiner", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string HasCombiner { get; set; }

        [MemberDoc("合路方式")]
        [AutoMapPropertyResolve("CombinerFunction", typeof(IndoorDistribution), typeof(CombinerFunctionDescriptionTransform))]
        public string CombinerFunctionDescription { get; set; }

        [MemberDoc("LTE是否合路老旧室分")]
        [AutoMapPropertyResolve("OldCombiner", typeof(IndoorDistribution), typeof(OldCombinerDescriptionTransform))]
        public string OldCombinerDescription { get; set; }

        [MemberDoc("L网信源数量")]
        public byte LteSources { get; set; }

        [MemberDoc("合路集成商")]
        public string CombinerIntegrator { get; set; }

        [MemberDoc("级别")]
        [AutoMapPropertyResolve("DistributionClass", typeof(IndoorDistribution), typeof(ENodebClassDescriptionTransform))]
        public string DistributionClassDescription { get; set; }

        [MemberDoc("巡检详细位置")]
        public string CheckingAddress { get; set; }

        [MemberDoc("是否与其他运营商合路")]
        [AutoMapPropertyResolve("IsCombinedWithOtherOperator", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string CombinedWithOtherOperator { get; set; }

        [MemberDoc("L网开通时间")]
        public DateTime? LteOpenDate { get; set; }

        [MemberDoc("单、双通道")]
        [AutoMapPropertyResolve("DistributionChannel", typeof(IndoorDistribution), typeof(DistributionChannelDescriptionTransform))]
        public string DistributionChannelDescription { get; set; }

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
        [AutoMapPropertyResolve("IsHasUnderGroundParker", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string HasUnderGroundParker { get; set; }

        [MemberDoc("覆盖面积")]
        public double? CoverageBuildingArea { get; set; }

        [MemberDoc("覆盖楼层数")]
        public int? CoverageFloors { get; set; }

        [MemberDoc("是否使用奇偶错层覆盖")]
        [AutoMapPropertyResolve("IsEvenOddFloorCoverage", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string EvenOddFloorCoverage { get; set; }

        [MemberDoc("LTE室分是否全覆盖")]
        [AutoMapPropertyResolve("IsLteFullCoverage", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string LteFullCoverage { get; set; }

        [MemberDoc("电梯是否LTE室分覆盖")]
        [AutoMapPropertyResolve("IsLiftLteFullCoverage", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string LiftLteFullCoverage { get; set; }

        [MemberDoc("地下室是否LTE室分覆盖")]
        [AutoMapPropertyResolve("IsUndergroundFullCoverage", typeof(IndoorDistribution), typeof(YesNoTransform))]
        public string UndergroundFullCoverage { get; set; }

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
        
    }
}
