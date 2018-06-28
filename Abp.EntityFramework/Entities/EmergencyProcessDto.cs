using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(EmergencyProcess))]
    [TypeDoc("应急需求流程处理信息")]
    public class EmergencyProcessDto
    {
        [MemberDoc("应急需求编号")]
        public int EmergencyId { get; set; }

        [AutoMapPropertyResolve("ProcessState", typeof(EmergencyProcess), typeof(EmergencyStateDescriptionTransform))]
        [MemberDoc("处理状态")]
        public string ProcessStateDescription { get; set; }

        [MemberDoc("处理时间")]
        public DateTime ProcessTime { get; set; }

        [MemberDoc("处理人")]
        public string ProcessPerson { get; set; }

        [MemberDoc("处理信息")]
        public string ProcessInfo { get; set; }

        [MemberDoc("附件文件路径")]
        public string AttachFilePath { get; set; }

        [MemberDoc("联系人")]
        public string ContactPerson { get; set; }
    }
}