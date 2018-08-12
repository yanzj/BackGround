using System;
using AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Antenna
{
    [EnumTypeDescription(typeof(AntennaPortsConfigure), Antenna2T4R)]
    public enum AntennaPortsConfigure
    {
        Antenna2T2R,
        Antenna2T4R,
        Antenna1T1R,
        Antenna2T8R,
        Antenna4T4R
    }

    public class AntennaPortsConfigureTransform : ValueResolver<string, AntennaPortsConfigure>
    {
        protected override AntennaPortsConfigure ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? AntennaPortsConfigure.Antenna2T4R : source.ToUpper().GetEnumType<AntennaPortsConfigure>();
        }
    }

    internal static class AntennaPortsConfigureTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaPortsConfigure.Antenna1T1R, "1T1R"),
                new Tuple<object, string>(AntennaPortsConfigure.Antenna2T2R, "2T2R"),
                new Tuple<object, string>(AntennaPortsConfigure.Antenna2T4R, "2T4R"),
                new Tuple<object, string>(AntennaPortsConfigure.Antenna2T8R, "2T8R"),
                new Tuple<object, string>(AntennaPortsConfigure.Antenna4T4R, "4T4R")
            };
        }
    }
}
