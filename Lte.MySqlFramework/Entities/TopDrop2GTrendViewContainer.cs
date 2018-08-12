using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(TopCellContainer<TopDrop2GTrend>))]
    public class TopDrop2GTrendViewContainer
    {
        [AutoMapPropertyResolve("TopCell", typeof(TopCellContainer<TopDrop2GTrend>))]
        public TopDrop2GTrendView TopDrop2GTrendView { get; set; }

        [AutoMapPropertyResolve("LteName", typeof(TopCellContainer<TopDrop2GTrend>))]
        public string ENodebName { get; set; }

        public string CdmaName { get; set; }

        public string CellName => CdmaName + "-" + TopDrop2GTrendView.SectorId;
    }
}