using Abp.EntityFramework.Entities;
using AutoMapper;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.AutoMapper
{
    public class ENodebExcelTransform : ValueResolver<ENodebExcel, ENodeb>
    {
        protected override ENodeb ResolveCore(ENodebExcel source)
        {
            return source.MapTo<ENodeb>();
        }
    }
}