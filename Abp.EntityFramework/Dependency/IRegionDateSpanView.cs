using System.Collections.Generic;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Dependency
{
    public interface IRegionDateSpanView<TDistrictView, TTownView> : IStatDate
        where TDistrictView : ICityDistrict
        where TTownView : ICityDistrictTown
    {
        IEnumerable<TDistrictView> DistrictViews { get; set; }

        IEnumerable<TTownView> TownViews { get; set; }
    }
}