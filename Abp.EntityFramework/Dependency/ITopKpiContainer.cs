using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Dependency
{
    public interface ITopKpiContainer<TStat>
        where TStat: class, IStatDate, ILteCellQuery, new()
    {
        TStat TopStat { get; set; }

        int TopDates { get; set; }
    }
}