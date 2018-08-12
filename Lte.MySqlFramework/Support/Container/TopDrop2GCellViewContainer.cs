using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.Container
{
    [AutoMapFrom(typeof(TopCellContainer<TopDrop2GCell>))]
    public class TopDrop2GCellViewContainer
    {
        [AutoMapPropertyResolve("TopCell", typeof(TopCellContainer<TopDrop2GCell>))]
        public TopDrop2GCellView TopDrop2GCellView { get; set; }

        public string LteName { get; set; }

        public string CdmaName { get; set; }
    }
}