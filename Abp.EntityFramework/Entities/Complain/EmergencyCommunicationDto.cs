using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(EmergencyCommunication))]
    [TypeDoc("Ӧ��ͨ�������¼")]
    public class EmergencyCommunicationDto : IDistrictTown, ITownId, IConstructDto<EmergencyProcessDto>, IStateChange
    {
        [MemberDoc("��¼���")]
        public int Id { get; set; }

        [AutoMapPropertyResolve("DemandLevel", typeof(EmergencyCommunication), typeof(DemandLevelDescriptionTransform))]
        [MemberDoc("����ȼ�")]
        public string DemandLevelDescription { get; set; }

        [MemberDoc("������������")]
        public string District { get; set; }

        [MemberDoc("������������")]
        public string Town { get; set; }

        [MemberDoc("�������")]
        public int TownId { get; set; }

        [MemberDoc("��Ŀ����")]
        public string ProjectName { get; set; }

        [MemberDoc("Ԥ������")]
        public int ExpectedPeople { get; set; }

        [MemberDoc("��ʼ����")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("��������")]
        public DateTime EndDate { get; set; }

        [AutoMapPropertyResolve("VehicleType", typeof(EmergencyCommunication), typeof(VehicularTypeDescriptionTransform))]
        [MemberDoc("Ӧ��������")]
        public string VehicularTypeDescription { get; set; }

        [MemberDoc("Ӧ��������")]
        public byte Vehicles { get; set; }

        [MemberDoc("���䷽ʽ")]
        public string TransmitFunction { get; set; }

        [MemberDoc("���緽ʽ")]
        public string ElectricSupply { get; set; }

        [MemberDoc("��������")]
        public string Area { get; set; }

        [MemberDoc("������")]
        public string Department { get; set; }

        [AutoMapPropertyResolve("ContactPerson", typeof(EmergencyCommunication), typeof(FirstLittleBracketContentsTransform))]
        [MemberDoc("��ϵ��")]
        public string Person { get; set; }

        [AutoMapPropertyResolve("ContactPerson", typeof(EmergencyCommunication), typeof(SecondLittleBracketContentsTransform))]
        [MemberDoc("��ϵ�绰")]
        public string Phone { get; set; }

        [MemberDoc("��ϵ��Ϣ")]
        public string ContactPerson => Person + "(" + Phone + ")";

        [AutoMapPropertyResolve("Description", typeof(EmergencyCommunication), typeof(FirstMiddleBracketContentsTransform))]
        [MemberDoc("ͨ�ų�λ��")]
        public string VehicleLocation { get; set; }

        [AutoMapPropertyResolve("Description", typeof(EmergencyCommunication), typeof(SecondMiddleBracketContentsTransform))]
        [MemberDoc("��������")]
        public string OtherDescription { get; set; }

        [MemberDoc("��Ҫ��Ϣ")]
        public string Description => "[" + VehicleLocation + "]" + OtherDescription;

        [AutoMapPropertyResolve("EmergencyState", typeof(EmergencyCommunication), typeof(EmergencyStateDescriptionTransform))]
        [MemberDoc("��ǰ״̬")]
        public string CurrentStateDescription { get; set; }

        [MemberDoc("��һ��״̬")]
        public string NextStateDescription
        {
            get
            {
                var nextState = CurrentStateDescription.GetNextStateDescription(EmergencyState.Finish);
                return nextState == null ? null : ((EmergencyState)nextState).GetEnumDescription();
            }
        }

        public EmergencyProcessDto Construct(string userName)
        {
            return new EmergencyProcessDto
            {
                EmergencyId = Id,
                ProcessPerson = userName,
                ProcessTime = DateTime.Now,
                ProcessStateDescription = CurrentStateDescription
            };
        }
    }
}