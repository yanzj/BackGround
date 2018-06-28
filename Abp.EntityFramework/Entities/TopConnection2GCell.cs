using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(TopConnection2GExcel))]
    public class TopConnection2GCell : Entity
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; }

        public int BtsId { get; set; }

        [AutoMapPropertyResolve("CellName", typeof(TopConnection2GExcel), typeof(FirstBracketCellIdTransform))]
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public short Frequency { get; set; }

        public int Drops { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

        public int TrafficAssignmentSuccess { get; set; }

        public int CallAttempts { get; set; }

        public int TrafficAssignmentFail { get; set; }
    }
}