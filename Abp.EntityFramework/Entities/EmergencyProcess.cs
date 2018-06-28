using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(EmergencyProcessDto))]
    public class EmergencyProcess : Entity
    {
        public int EmergencyId { get; set; }

        [AutoMapPropertyResolve("ProcessStateDescription", typeof(EmergencyProcessDto), typeof(EmergencyStateTransform))]
        public EmergencyState ProcessState { get; set; }

        public DateTime ProcessTime { get; set; }

        public string ProcessPerson { get; set; }

        public string ProcessInfo { get; set; }

        public string AttachFilePath { get; set; }

        public string ContactPerson { get; set; }
    }
}