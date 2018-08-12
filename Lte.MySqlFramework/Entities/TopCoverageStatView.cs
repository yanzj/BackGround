using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Test;
using AutoMapper;

namespace Lte.MySqlFramework.Entities
{
    public class TopCoverageStatView : CoverageStatView
    {
        public int TopDates { get; set; }

        public static TopCoverageStatView ConstructView(CoverageStat stat, IEnumerable<ENodeb> eNodebs)
        {
            var view = Mapper.Map<CoverageStat, TopCoverageStatView>(stat);
            var eNodeb = eNodebs.FirstOrDefault(x => x.ENodebId == stat.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }

    }
}