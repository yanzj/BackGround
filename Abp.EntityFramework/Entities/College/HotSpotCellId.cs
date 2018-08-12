using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities.College
{
    [AutoMapFrom(typeof(HotSpotCellExcel))]
    public class HotSpotCellId : Entity, IHotSpot, ILteCellQuery
    {
        [AutoMapPropertyResolve("HotSpotTypeDescription", typeof(HotSpotCellExcel), typeof(HotspotTypeTransform))]
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.Cell;

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }
    }
}