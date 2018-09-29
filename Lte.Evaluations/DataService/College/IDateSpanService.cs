using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService.College
{
    public interface IDateSpanService<TItem>
    {
        List<TItem> QueryItems(DateTime begin, DateTime end);

        Task<int> QueryCount(DateTime begin, DateTime end);
    }
}