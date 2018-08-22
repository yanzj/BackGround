using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Cdma;

namespace Lte.MySqlFramework.Support.Container
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