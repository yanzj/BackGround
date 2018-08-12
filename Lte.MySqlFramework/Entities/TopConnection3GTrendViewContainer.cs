using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(TopCellContainer<TopConnection3GTrend>))]
    public class TopConnection3GTrendViewContainer
    {
        [AutoMapPropertyResolve("TopCell", typeof(TopCellContainer<TopConnection3GTrend>))]
        public TopConnection3GTrendView TopConnection3GTrendView { get; set; }

        [AutoMapPropertyResolve("LteName", typeof(TopCellContainer<TopConnection3GTrend>))]
        public string ENodebName { get; set; }

        public string CdmaName { get; set; }

        public string CellName => CdmaName + "-" + TopConnection3GTrendView.SectorId;
    }
}