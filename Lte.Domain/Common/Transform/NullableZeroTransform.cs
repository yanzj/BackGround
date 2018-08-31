using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    /// <summary>
    /// 双精度转双精度
    /// </summary>
    public class NullableZeroTransform : ValueResolver<double?, double>
    {
        protected override double ResolveCore(double? source)
        {
            return source ?? 0;
        }
    }
}