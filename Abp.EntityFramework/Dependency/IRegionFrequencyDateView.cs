using System;
using System.Collections.Generic;

namespace Abp.EntityFramework.Dependency
{
    public interface IRegionFrequencyDateView<TView>
        where TView: IStatDate, IFrequencyBand
    {
        DateTime StatDate { get; set; }

        string Region { get; set; }

        IEnumerable<TView> FrequencyViews { get; set; }
    }
}