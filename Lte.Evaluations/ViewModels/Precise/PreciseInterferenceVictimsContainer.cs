using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class PreciseInterferenceVictimsContainer : IPreciseWorkItemDto<PreciseInterferenceVictimDto>
    {
        public List<PreciseInterferenceVictimDto> Items { get; set; }

        public string WorkItemNumber { get; set; }
    }
}