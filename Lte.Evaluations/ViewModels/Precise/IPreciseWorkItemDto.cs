using System.Collections.Generic;
using Lte.Domain.Common.Wireless;

namespace Lte.Evaluations.ViewModels.Precise
{
    public interface IPreciseWorkItemDto<TLteCellId>
        where TLteCellId : class, ILteCellQuery, new()
    {
        List<TLteCellId> Items { get; set; }

        string WorkItemNumber { get; set; }
    }
}