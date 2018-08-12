using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities.Cdma
{
    public class CdmaBtsTransform : ValueResolver<BtsExcel, CdmaBts>
    {
        protected override CdmaBts ResolveCore(BtsExcel source)
        {
            return source.MapTo<CdmaBts>();
        }
    }
}