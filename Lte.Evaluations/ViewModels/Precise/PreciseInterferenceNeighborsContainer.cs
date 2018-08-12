using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class PreciseInterferenceNeighborsContainer : IPreciseWorkItemDto<PreciseInterferenceNeighborDto>
    {
        public List<PreciseInterferenceNeighborDto> Items { get; set; }

        public string WorkItemNumber { get; set; }
    }
}