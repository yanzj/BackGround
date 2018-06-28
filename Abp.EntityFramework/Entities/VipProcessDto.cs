using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Entities;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(VipProcess))]
    public class VipProcessDto
    {
        public string SerialNumber { get; set; }

        public string ProcessInfo { get; set; }

        [MemberDoc("是否转需求单")]
        [AutoMapPropertyResolve("IsTransferedToDemand", typeof(VipProcess), typeof(YesNoTransform))]
        public string TransferedToDemand { get; set; }

        [MemberDoc("咨询话务员是否核查覆盖情况")]
        [AutoMapPropertyResolve("CoverageIsChecked", typeof(VipProcess), typeof(YesNoTransform))]
        public string CoverageChecked { get; set; }

        [MemberDoc("预处理是否成功")]
        [AutoMapPropertyResolve("IsPreprocessed", typeof(VipProcess), typeof(YesNoTransform))]
        public string Preprocessed { get; set; }

        [MemberDoc("地标信息补登")]
        public string LandscapeInfo { get; set; }

        [MemberDoc("后续跟进")]
        public string ProcessStageInfo { get; set; }

        [MemberDoc("是否联系用户")]
        [AutoMapPropertyResolve("IsUserTriedToBeContacted", typeof(VipProcess), typeof(YesNoTransform))]
        public string UserTriedToBeContacted { get; set; }

        [MemberDoc("联系用户的电话号码（我方）")]
        public string ContactStaffPhoneNumber { get; set; }

        [MemberDoc("联系用户时间")]
        public DateTime? ContactUserTime { get; set; }

        [MemberDoc("联系用户的人员（我方人员）")]
        public string ContactStaffName { get; set; }

        [MemberDoc("能否联系上用户")]
        [AutoMapPropertyResolve("IsUserContacted", typeof(VipProcess), typeof(YesNoTransform))]
        public string UserContacted { get; set; }

        [MemberDoc("与用户沟通信息")]
        public string UserContactInfo { get; set; }

        [MemberDoc("用户是否接受")]
        [AutoMapPropertyResolve("IsUserAccept", typeof(VipProcess), typeof(YesNoTransform))]
        public string UserAccept { get; set; }

        [MemberDoc("未能联系用户，是否发短信给用户")]
        public string ShortMessageInfo { get; set; }

        [AutoMapPropertyResolve("ShouldSendMessage", typeof(VipProcess), typeof(YesNoTransform))]
        public string SendMessage { get; set; }

        [MemberDoc("短信格式（已做格式）")]
        public string ShortMessageFormat { get; set; }

        [MemberDoc("截图是否上传服务器")]
        [AutoMapPropertyResolve("IsPictureUploaded", typeof(VipProcess), typeof(YesNoTransform))]
        public string PictureUploaded { get; set; }

        public string PicturePath { get; set; }

        [MemberDoc("处理结果")]
        public string ProcessResult { get; set; }

        [MemberDoc("是否闭环")]
        [AutoMapPropertyResolve("IsResolved", typeof(VipProcess), typeof(YesNoTransform))]
        public string Resolved { get; set; }

        [MemberDoc("闭环时间")]
        public DateTime? ResolveTime { get; set; }
    }
}