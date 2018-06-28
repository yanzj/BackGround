using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(CellExcel))]
    public class ENodebBtsIdPair
    {
        public int ENodebId { get; set; }

        [AutoMapPropertyResolve("ShareCdmaInfo", typeof(CellExcel), typeof(SharedBtsIdTransform))]
        public int BtsId { get; set; }
    }
}