using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class PreciseCoveragesContainer : IPreciseWorkItemDto<PreciseCoverageDto>
    {
        public List<PreciseCoverageDto> Items { get; set; }

        public string WorkItemNumber { get; set; }
    }
}