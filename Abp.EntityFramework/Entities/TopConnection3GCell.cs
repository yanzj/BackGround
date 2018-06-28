using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(TopConnection3GCellExcel))]
    public class TopConnection3GCell : Entity, IBtsIdQuery
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; }

        public int BtsId { get; set; }

        [AutoMapPropertyResolve("CellName", typeof(TopConnection3GCellExcel), typeof(FirstBracketCellIdTransform))]
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int WirelessDrop { get; set; }

        public int ConnectionAttempts { get; set; }

        public int ConnectionFails { get; set; }

        public double LinkBusyRate { get; set; }
    }
}