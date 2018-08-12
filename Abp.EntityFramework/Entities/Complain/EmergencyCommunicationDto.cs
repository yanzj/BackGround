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
    [TypeDoc("应急通信需求记录")]
    public class EmergencyCommunicationDto : IDistrictTown, ITownId, IConstructDto<EmergencyProcessDto>, IStateChange
    {
        [MemberDoc("记录编号")]
        public int Id { get; set; }

        [AutoMapPropertyResolve("DemandLevel", typeof(EmergencyCommunication), typeof(DemandLevelDescriptionTransform))]
        [MemberDoc("需求等级")]
        public string DemandLevelDescription { get; set; }

        [MemberDoc("需求所在区域")]
        public string District { get; set; }

        [MemberDoc("需求所在镇区")]
        public string Town { get; set; }

        [MemberDoc("镇区编号")]
        public int TownId { get; set; }

        [MemberDoc("项目名称")]
        public string ProjectName { get; set; }

        [MemberDoc("预计人数")]
        public int ExpectedPeople { get; set; }

        [MemberDoc("开始日期")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("结束日期")]
        public DateTime EndDate { get; set; }

        [AutoMapPropertyResolve("VehicleType", typeof(EmergencyCommunication), typeof(VehicularTypeDescriptionTransform))]
        [MemberDoc("应急车类型")]
        public string VehicularTypeDescription { get; set; }

        [MemberDoc("应急车数量")]
        public byte Vehicles { get; set; }

        [MemberDoc("传输方式")]
        public string TransmitFunction { get; set; }

        [MemberDoc("供电方式")]
        public string ElectricSupply { get; set; }

        [MemberDoc("服务区域")]
        public string Area { get; set; }

        [MemberDoc("需求部门")]
        public string Department { get; set; }

        [AutoMapPropertyResolve("ContactPerson", typeof(EmergencyCommunication), typeof(FirstLittleBracketContentsTransform))]
        [MemberDoc("联系人")]
        public string Person { get; set; }

        [AutoMapPropertyResolve("ContactPerson", typeof(EmergencyCommunication), typeof(SecondLittleBracketContentsTransform))]
        [MemberDoc("联系电话")]
        public string Phone { get; set; }

        [MemberDoc("联系信息")]
        public string ContactPerson => Person + "(" + Phone + ")";

        [AutoMapPropertyResolve("Description", typeof(EmergencyCommunication), typeof(FirstMiddleBracketContentsTransform))]
        [MemberDoc("通信车位置")]
        public string VehicleLocation { get; set; }

        [AutoMapPropertyResolve("Description", typeof(EmergencyCommunication), typeof(SecondMiddleBracketContentsTransform))]
        [MemberDoc("其他描述")]
        public string OtherDescription { get; set; }

        [MemberDoc("主要信息")]
        public string Description => "[" + VehicleLocation + "]" + OtherDescription;

        [AutoMapPropertyResolve("EmergencyState", typeof(EmergencyCommunication), typeof(EmergencyStateDescriptionTransform))]
        [MemberDoc("当前状态")]
        public string CurrentStateDescription { get; set; }

        [MemberDoc("下一步状态")]
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