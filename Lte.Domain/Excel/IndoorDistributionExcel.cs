using System;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class IndoorDistributionExcel : IGeoPoint<double>
    {
        [ExcelColumn("室分编码")]
        public string IndoorSerialNum { get; set; }
        
        [ExcelColumn("室分地址")]
        public string Address { get; set; }

        [ExcelColumn("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [ExcelColumn("乡/镇/街道")]
        public string StationTown { get; set; }

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("小区标识")]
        public string CellSerialNum { get; set; }

        [ExcelColumn("RRU标识")]
        public string RruSerialNum { get; set; }

        [ExcelColumn("室分性质")]
        public string IndoorCategoryDescription { get; set; }

        [ExcelColumn("覆盖区域")]
        public string CoverageArea { get; set; }

        [ExcelColumn("集成商")]
        public string Integritor { get; set; }

        [ExcelColumn("分布系统性质")]
        public string IndoorNetworkDescription { get; set; }

        [ExcelColumn("是否合路")]
        public string HasCombiner { get; set; }

        [ExcelColumn("合路方式")]
        public string CombinerFunctionDescription { get; set; }

        [ExcelColumn("LTE是否合路老旧室分")]
        public string OldCombinerDescription { get; set; }

        [ExcelColumn("L网信源数量")]
        public byte LteSources { get; set; }

        [ExcelColumn("L网开通时间")]
        public DateTime? LteOpenDate { get; set; }

        [ExcelColumn("单、双通道")]
        public string DistributionChannelDescription { get; set; }

        [ExcelColumn("楼宇名称")]
        public string BuildingName { get; set; }

        [ExcelColumn("楼宇编码")]
        public string BuildingCode { get; set; }

        [ExcelColumn("楼宇地址")]
        public string BuildingAddress { get; set; }

        [ExcelColumn("电梯数")]
        public byte? TotalLifts { get; set; }

        [ExcelColumn("面积")]
        public double? BuildingArea { get; set; }

        [ExcelColumn("层数")]
        public int TotalFloors { get; set; }

        [ExcelColumn("楼内是否有地下停车场")]
        public string HasUnderGroundParker { get; set; }

        [ExcelColumn("覆盖面积")]
        public double? CoverageBuildingArea { get; set; }

        [ExcelColumn("覆盖楼层数")]
        public int? CoverageFloors { get; set; }

        [ExcelColumn("是否使用奇偶错层覆盖")]
        public string EvenOddFloorCoverage { get; set; }

        [ExcelColumn("LTE室分是否全覆盖")]
        public string LteFullCoverage { get; set; }

        [ExcelColumn("电梯是否LTE室分覆盖")]
        public string LiftLteFullCoverage { get; set; }

        [ExcelColumn("地下室是否LTE室分覆盖")]
        public string UndergroundFullCoverage { get; set; }

        [ExcelColumn("其它未覆盖区域描述")]
        public string OtherNoCoverageComments { get; set; }

        [ExcelColumn("停车场覆盖情况")]
        public string ParkCoverages { get; set; }

        [ExcelColumn("业主")]
        public string Yezhu { get; set; }

        [ExcelColumn("电话")]
        public string YezhuPhone { get; set; }

        [ExcelColumn("初次设计文件")]
        public string FirstDesignFile { get; set; }

        [ExcelColumn("初次验收文件")]
        public string FirstYanshouFile { get; set; }

        [ExcelColumn("改造设计文件")]
        public string ModifyDesignFile { get; set; }

        [ExcelColumn("改造设验收")]
        public string ModifyYanshouFile { get; set; }

        [ExcelColumn("维护人")]
        public string Maintainor { get; set; }

        [ExcelColumn("维护人联系方式")]
        public string MaintainContact { get; set; }

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