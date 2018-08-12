using AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    public abstract class DescriptionTransform<TEnum> : ValueResolver<TEnum, string>
        where TEnum : struct 
    {
        protected override string ResolveCore(TEnum source)
        {
            return source.GetEnumDescription();
        }
    }

    public abstract class EnumTransform<TEnum> : ValueResolver<string, TEnum>
        where TEnum : struct
    {
        private readonly TEnum _defaultEnum;

        protected EnumTransform(TEnum defaultEnum)
        {
            _defaultEnum = defaultEnum;
        } 

        protected override TEnum ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? _defaultEnum : source.GetEnumType<TEnum>();
        }
    }

}
