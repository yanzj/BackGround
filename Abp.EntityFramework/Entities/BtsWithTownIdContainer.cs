using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(BtsExcelWithTownIdContainer))]
    public class BtsWithTownIdContainer
    {
        [AutoMapPropertyResolve("BtsExcel", typeof(BtsExcelWithTownIdContainer), typeof(CdmaBtsTransform))]
        public CdmaBts CdmaBts { get; set; }

        public int TownId { get; set; }
    }
}