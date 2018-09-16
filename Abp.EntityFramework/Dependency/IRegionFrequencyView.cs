using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Abp.EntityFramework.Dependency
{
    public interface IRegionFrequencyView<TView>
    where TView: IStatTime, IFrequencyBand
    {
        DateTime StatDate { get; set; }

        string Region { get; set; }

        IEnumerable<TView> FrequencyViews { get; set; }
    }
}
