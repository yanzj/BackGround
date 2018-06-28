using System;
using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities
{
    public class EmergencyFiberWorkItem : Entity
    {
        public int EmergencyId { get; set; }

        public string WorkItemNumber { get; set; }

        public string Person { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}