using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(InfrastructureType), Unknown)]
    public enum InfrastructureType : byte
    {
        ENodeb,
        Cell,
        CdmaBts,
        CdmaCell,
        LteIndoor,
        CdmaIndoor,
        HotSpot,
        Unknown,
        DenseCity,
        City,
        Suburb,
        Rural,
        PerformanceInform
    }

    public class InfrastructureTypeDescriptionTransform : DescriptionTransform<InfrastructureType>
    {

    }

    public class InfrastructureTypeTransform : EnumTransform<InfrastructureType>
    {
        public InfrastructureTypeTransform() : base(InfrastructureType.Unknown)
        {
        }
    }

    internal static class InfrastructureTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(InfrastructureType.ENodeb, "LTE基站"),
                new Tuple<object, string>(InfrastructureType.ENodeb, "eNodeB"),
                new Tuple<object, string>(InfrastructureType.Cell, "LTE小区"),
                new Tuple<object, string>(InfrastructureType.CdmaBts, "CDMA基站"),
                new Tuple<object, string>(InfrastructureType.CdmaBts, "基站"),
                new Tuple<object, string>(InfrastructureType.CdmaCell, "CDMA小区"),
                new Tuple<object, string>(InfrastructureType.LteIndoor, "LTE室内分布"),
                new Tuple<object, string>(InfrastructureType.CdmaIndoor, "CDMA室内分布"),
                new Tuple<object, string>(InfrastructureType.HotSpot, "热点"),
                new Tuple<object, string>(InfrastructureType.Unknown, "未知"),
                new Tuple<object, string>(InfrastructureType.DenseCity, "密集城区"), //8
                new Tuple<object, string>(InfrastructureType.City, "一般城区"),
                new Tuple<object, string>(InfrastructureType.Suburb, "郊区"),
                new Tuple<object, string>(InfrastructureType.Rural, "农村开阔地"),
                new Tuple<object, string>(InfrastructureType.PerformanceInform, "性能通知")
            };
        }
    }
}
