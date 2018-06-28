using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using AutoMapper;
using Lte.Evaluations.Policy;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        protected override void Configure()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }
    }
}
