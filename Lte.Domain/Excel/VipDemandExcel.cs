using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class VipDemandExcel : IBeginDate, IDistrictTown
    {
        [ExcelColumn("日期")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("工单编号")]
        public string WorkItemNumber { get; set; }

        [ExcelColumn("是否录音")]
        public string Recording { get; set; }

        public bool IsRecording => Recording == "是";

        [ExcelColumn("联系电话")]
        public string PhoneNumber { get; set; }

        [ExcelColumn("联系人")]
        public string ContactPerson { get; set; }

        [ExcelColumn("话务员工号")]
        public string ReceiptNumber { get; set; }

        [ExcelColumn("受理班组")]
        public string Department { get; set; }

        [ExcelColumn("工作区域")]
        public string Area { get; set; }

        [ExcelColumn("求助佛山是否正确")]
        public string BelongedCity { get; set; }

        [ExcelColumn("预处理（流水号）单号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("现象")]
        public string Phenomenon { get; set; }

        [ExcelColumn("初步处理结果")]
        public string FinishResults { get; set; }

        [ExcelColumn("值班人员")]
        public string SustainPerson { get; set; }

        [ExcelColumn("工号")]
        public string StaffId { get; set; }

        [ExcelColumn("类型")]
        public string ComplainCategoryDescription { get; set; }

        [ExcelColumn("投诉地址")]
        public string ProjectName { get; set; }

        [ExcelColumn("话务员咨询问题")]
        public string ProjectContents { get; set; }

        [ExcelColumn("区域")]
        public string District { get; set; }

        [ExcelColumn("镇街")]
        public string Town { get; set; }

        [ExcelColumn("专家答复、建议")]
        public string ProcessInfo { get; set; }

        [ExcelColumn("地标信息补登")]
        public string LandscapeInfo { get; set; }

        [ExcelColumn("后续跟进")]
        public string ProcessStageInfo { get; set; }

        [ExcelColumn("联系用户的电话号码（我方）")]
        public string ContactStaffPhoneNumber { get; set; }

        [ExcelColumn("联系用户时间")]
        public DateTime? ContactUserTime { get; set; }

        [ExcelColumn("联系用户的人员（我方人员）")]
        public string ContactStaffName { get; set; }
        
        [ExcelColumn("未能联系用户，是否发短信给用户")]
        public string ShortMessageInfo { get; set; }

        public bool ShouldSendMessage => !string.IsNullOrEmpty(ShortMessageInfo) && ShortMessageInfo != "否";

        [ExcelColumn("现场人员反馈信息")]
        public string ProcessResult { get; set; }

        [ExcelColumn("用户是否接受")]
        public string UserAccept { get; set; }

        [ExcelColumn("短信格式（已做格式）")]
        public string ShortMessageFormat { get; set; }
    }
}