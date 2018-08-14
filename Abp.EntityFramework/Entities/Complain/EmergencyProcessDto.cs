using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(EmergencyProcess))]
    [TypeDoc("Ӧ���������̴�����Ϣ")]
    public class EmergencyProcessDto
    {
        [MemberDoc("Ӧ��������")]
        public int EmergencyId { get; set; }

        [AutoMapPropertyResolve("ProcessState", typeof(EmergencyProcess), typeof(EmergencyStateDescriptionTransform))]
        [MemberDoc("����״̬")]
        public string ProcessStateDescription { get; set; }

        [MemberDoc("����ʱ��")]
        public DateTime ProcessTime { get; set; }

        [MemberDoc("������")]
        public string ProcessPerson { get; set; }

        [MemberDoc("������Ϣ")]
        public string ProcessInfo { get; set; }

        [MemberDoc("�����ļ�·��")]
        public string AttachFilePath { get; set; }

        [MemberDoc("��ϵ��")]
        public string ContactPerson { get; set; }
    }
}