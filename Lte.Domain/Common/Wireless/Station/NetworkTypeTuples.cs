using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(NetworkType), Others)]
    public enum NetworkType : byte
    {
        With4G,
        With2G3G4G,
        With2G3G,
        With2G,
        With3G,
        With2G3G4G4GPlus,
        Volte,
        NbIot,
        Others,
        Lte,
        Wifi,
        Evdo
    }

    public class NetworkTypeDescriptionTransform : DescriptionTransform<NetworkType>
    {

    }

    public class NetworkTypeTransform : EnumTransform<NetworkType>
    {
        public NetworkTypeTransform() : base(NetworkType.Others)
        {
        }
    }

    internal static class NetworkTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(NetworkType.With2G, "2G"),
                new Tuple<object, string>(NetworkType.With2G3G, "2G/3G"),
                new Tuple<object, string>(NetworkType.With2G3G4G, "2G/3G/4G"),
                new Tuple<object, string>(NetworkType.With2G3G4G4GPlus, "2G/3G/4G/4G+"),
                new Tuple<object, string>(NetworkType.With3G, "3G"),
                new Tuple<object, string>(NetworkType.With4G, "4G"),
                new Tuple<object, string>(NetworkType.With3G, "3G网络"),
                new Tuple<object, string>(NetworkType.With4G, "4G网络"),
                new Tuple<object, string>(NetworkType.Volte, "VoLTE"), 
                new Tuple<object, string>(NetworkType.NbIot, "NB-IoT"), 
                new Tuple<object, string>(NetworkType.Lte, "LTE"), 
                new Tuple<object, string>(NetworkType.Wifi, "WIFI"),
                new Tuple<object, string>(NetworkType.Evdo, "EVDO"), 
            };
        }
    }
}
