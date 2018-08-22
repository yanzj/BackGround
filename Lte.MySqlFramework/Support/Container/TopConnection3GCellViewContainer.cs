using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Cdma;

namespace Lte.MySqlFramework.Support.Container
{
    [AutoMapFrom(typeof(TopCellContainer<TopConnection3GCell>))]
    public class TopConnection3GCellViewContainer
    {
        [AutoMapPropertyResolve("TopCell", typeof(TopCellContainer<TopConnection3GCell>))]
        public TopConnection3GCellView TopConnection3GCellView { get; set; }

        public string LteName { get; set; }

        public string CdmaName { get; set; }
    }
}