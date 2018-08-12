using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities.Cdma
{
    [AutoMapFrom(typeof(CdmaCellExcel))]
    public class CdmaRru : Entity
    {
        public int BtsId { get; set; }

        public byte SectorId { get; set; }

        public byte TrmId { get; set; }

        public string RruName { get; set; }
    }
}