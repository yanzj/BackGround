using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Region;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(EmergencyCommunicationDto))]
    public class EmergencyCommunication : Entity
    {
        [AutoMapPropertyResolve("DemandLevelDescription", typeof(EmergencyCommunicationDto), typeof(DemandLevelTransform))]
        public DemandLevel DemandLevel { get; set; }

        public int TownId { get; set; }

        public string ProjectName { get; set; }

        public int ExpectedPeople { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [AutoMapPropertyResolve("VehicularTypeDescription", typeof(EmergencyCommunicationDto), typeof(VehicularTypeTransform))]
        public VehicleType VehicleType { get; set; }

        public byte Vehicles { get; set; }

        public string TransmitFunction { get; set; }

        public string ElectricSupply { get; set; }

        public string Area { get; set; }

        public string Department { get; set; }

        public string ContactPerson { get; set; }

        public string Description { get; set; }

        [AutoMapPropertyResolve("CurrentStateDescription", typeof(EmergencyCommunicationDto), typeof(EmergencyStateTransform))]
        public EmergencyState EmergencyState { get; set; }
    }
}