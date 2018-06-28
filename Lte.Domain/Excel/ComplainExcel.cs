using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class ComplainExcel : IBeginDate
    {
        [ExcelColumn("工单编号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("所属区域")]
        public string SelfDistrict { get; set; }

        public string District
            =>
                string.IsNullOrEmpty(SelfDistrict)
                    ? (string.IsNullOrEmpty(Grid)
                        ? (string.IsNullOrEmpty(CandidateDistrict) ? "" : CandidateDistrict.Replace("区", ""))
                        : Grid.Replace("FS", ""))
                    : SelfDistrict;

        [ExcelColumn("产品类型")]
        public string ProductType { get; set; }

        [ExcelColumn("服务类别")]
        public string ServiceType { get; set; }

        public string[] ServiceTypeFields => ServiceType.GetSplittedFields('-');

        public string ServiceType1 => ServiceTypeFields.Length > 0 ? ServiceTypeFields[0] : "";

        public string ServiceType2 => ServiceTypeFields.Length > 1 ? ServiceTypeFields[1] : "";

        public string ServiceType3 => ServiceTypeFields.Length > 2 ? ServiceTypeFields[2] : "";

        [ExcelColumn("工单状态")]
        public string StateDescription { get; set; }

        [ExcelColumn("客户电话")]
        public string SubscriberPhone { get; set; }

        [ExcelColumn("重复次数")]
        public byte? RepeatTimes { get; set; }

        [ExcelColumn("紧急程度")]
        public string UrgentDescription { get; set; }

        public bool IsUrgent => UrgentDescription == "紧急";

        [ExcelColumn("工单来源")]
        public string ComplainSource { get; set; }

        [ExcelColumn("归属地")]
        public string City { get; set; }

        [ExcelColumn("客户名称")]
        public string SubscriberInfo { get; set; }

        [ExcelColumn("联系电话1")]
        public string ContactPhone { get; set; }

        [ExcelColumn("联系人")]
        public string ContactPerson { get; set; }

        [ExcelColumn("联系地址")]
        public string ContactAddress { get; set; }

        [ExcelColumn("受理内容")]
        public string ComplainContents { get; set; }

        [ExcelColumn("受理时间")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("受理人班组")]
        public string ManagerInfo { get; set; }

        [ExcelColumn("当前环节")]
        public string StageDescription { get; set; }

        [ExcelColumn("全程超时时间")]
        public DateTime Deadline { get; set; }

        [ExcelColumn("当前处理班组")]
        public string CurrentProcessor { get; set; }

        [ExcelColumn("当前班组接单时间")]
        public DateTime ProcessTime { get; set; }

        [ExcelColumn("电子运维流水号")]
        public string OssSerialNumber { get; set; }

        [ExcelColumn("工单来源")]
        public string SourceDescription { get; set; }

        [ExcelColumn("受理时间")]
        public DateTime BeginTime { get; set; }

        [ExcelColumn("区/县")]
        public string CandidateDistrict { get; set; }

        [ExcelColumn("路名")]
        public string RoadName { get; set; }

        [ExcelColumn("楼宇名称")]
        public string BuildingName { get; set; }

        [ExcelColumn("原因定性")]
        public string CauseLocation { get; set; }

        [ExcelColumn("预处理内容")]
        public string PreProcessContents { get; set; }

        [ExcelColumn("4G用户")]
        public string Subscriber4G { get; set; }

        [ExcelColumn("经度", TransformEnum.Longtitute, 0)]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度", TransformEnum.Lattitute, 0)]
        public double Lattitute { get; set; }

        [ExcelColumn("原因定性一级")]
        public string FirstReason { get; set; }

        public string ReasonFirst => string.IsNullOrEmpty(FirstReason) ? ServiceType2 : FirstReason;

        [ExcelColumn("原因定性二级")]
        public string SecondReason { get; set; }

        public string ReasonSecond => string.IsNullOrEmpty(SecondReason) ? ServiceType3 : SecondReason;

        [ExcelColumn("归属网格")]
        public string Grid { get; set; }

        [ExcelColumn("业务类型")]
        public string NetworkDescription { get; set; }

        [ExcelColumn("相关站点名称")]
        public string Site { get; set; }

        [ExcelColumn("问题点或区域描述")]
        public string Position { get; set; }

        public string SitePosition => string.IsNullOrEmpty(Site) ? Position : Site;

        [ExcelColumn("室内室外")]
        public string IndoorDescription { get; set; }

        [ExcelColumn("使用场合")]
        public string Scene { get; set; }

        [ExcelColumn("表象大类")]
        public string CategoryDescription { get; set; }

        public int TownId { get; set; }
    }
}