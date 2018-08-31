using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(VipDemandExcel), typeof(VipProcessDto))]
    [TypeDoc("在线求助信息视图")]
    public class VipProcess : Entity
    {
        public string SerialNumber { get; set; }

        public string ProcessInfo { get; set; }

        [MemberDoc("是否转需求单")]
        [AutoMapPropertyResolve("TransferedToDemand", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsTransferedToDemand { get; set; }

        [MemberDoc("咨询话务员是否核查覆盖情况")]
        [AutoMapPropertyResolve("CoverageChecked", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool CoverageIsChecked { get; set; }

        [MemberDoc("预处理是否成功")]
        [AutoMapPropertyResolve("Preprocessed", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsPreprocessed { get; set; }

        [MemberDoc("地标信息补登")]
        public string LandscapeInfo { get; set; }

        [MemberDoc("后续跟进")]
        public string ProcessStageInfo { get; set; }

        [MemberDoc("是否联系用户")]
        [AutoMapPropertyResolve("UserTriedToBeContacted", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsUserTriedToBeContacted { get; set; }

        [MemberDoc("联系用户的电话号码（我方）")]
        public string ContactStaffPhoneNumber { get; set; }

        [MemberDoc("联系用户时间")]
        public DateTime? ContactUserTime { get; set; }

        [MemberDoc("联系用户的人员（我方人员）")]
        public string ContactStaffName { get; set; }

        [MemberDoc("能否联系上用户")]
        [AutoMapPropertyResolve("UserContacted", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsUserContacted { get; set; }

        [MemberDoc("与用户沟通信息")]
        public string UserContactInfo { get; set; }

        [MemberDoc("用户是否接受")]
        [AutoMapPropertyResolve("UserAccept", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        [AutoMapPropertyResolve("UserAccept", typeof(VipDemandExcel), typeof(YesToBoolTransform))]
        public bool IsUserAccept { get; set; }

        [MemberDoc("未能联系用户，是否发短信给用户")]
        public string ShortMessageInfo { get; set; }

        [AutoMapPropertyResolve("SendMessage", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool ShouldSendMessage { get; set; }

        [MemberDoc("短信格式（已做格式）")]
        public string ShortMessageFormat { get; set; }

        [MemberDoc("截图是否上传服务器")]
        [AutoMapPropertyResolve("PictureUploaded", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsPictureUploaded { get; set; }

        public string PicturePath { get; set; }

        [MemberDoc("现场人员反馈信息")]
        public string ProcessResult { get; set; }

        [MemberDoc("是否闭环")]
        [AutoMapPropertyResolve("Resolved", typeof(VipProcessDto), typeof(YesToBoolTransform))]
        public bool IsResolved { get; set; }

        [MemberDoc("闭环时间")]
        public DateTime? ResolveTime { get; set; }
    }
}