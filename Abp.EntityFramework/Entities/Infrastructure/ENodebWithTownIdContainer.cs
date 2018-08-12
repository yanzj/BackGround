using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(ENodebExcelWithTownIdContainer))]
    public class ENodebWithTownIdContainer
    {
        [AutoMapPropertyResolve("ENodebExcel", typeof(ENodebExcelWithTownIdContainer), typeof(ENodebExcelTransform))]
        public ENodeb ENodeb { get; set; }

        public int TownId { get; set; }
    }
}