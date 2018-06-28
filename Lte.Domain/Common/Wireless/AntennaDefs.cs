using AutoMapper;

namespace Lte.Domain.Common.Wireless
{
    public enum FrequencyBandType
    {
        Downlink2100,
        Uplink2100,
        Downlink1800,
        Uplink1800,
        Tdd2600,
        Undefined,
        All,
        Band2100,
        Band1800,
        Band800VoLte,
        Band800NbIot,
        College
    }

    internal class FrequencyBandDef
    {
        public FrequencyBandType FrequencyBandType { get; set; }

        public int FcnStart { get; set; }

        public int FcnEnd { get; set; }

        public double FrequencyStart { get; set; }

        public double FrequencyEnd { get; set; }
    }

    public enum RegionType : byte
    {
        Circle,
        Rectangle,
        Polygon,
        PolyLine
    }

    public class RectangleRange
    {
        public double West { get; set; }

        public double East { get; set; }

        public double South { get; set; }

        public double North { get; set; }
    }
}
