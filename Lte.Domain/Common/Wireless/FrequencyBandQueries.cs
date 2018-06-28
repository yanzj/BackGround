using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.Common.Wireless
{
    public static class FrequencyBandQueries
    {
        private static readonly List<FrequencyBandDef> frequencyBands = new List<FrequencyBandDef>{
            new FrequencyBandDef {
                FrequencyBandType = FrequencyBandType.Downlink1800,
                FrequencyStart = 1805,
                FrequencyEnd = 1880,
                FcnStart = 1200,
                FcnEnd = 1950 },
            new FrequencyBandDef {
                FrequencyBandType = FrequencyBandType.Uplink1800,
                FrequencyStart = 1710,
                FrequencyEnd = 1785,
                FcnStart = 19200,
                FcnEnd = 19950 },
            new FrequencyBandDef {
                FrequencyBandType = FrequencyBandType.Tdd2600,
                FrequencyStart = 2620,
                FrequencyEnd = 2690,
                FcnStart = 2750,
                FcnEnd = 3450 },
            new FrequencyBandDef {
                FrequencyBandType = FrequencyBandType.Downlink2100,
                FrequencyStart = 2110,
                FrequencyEnd = 2170,
                FcnStart = 0,
                FcnEnd = 600 },
            new FrequencyBandDef {
                FrequencyBandType = FrequencyBandType.Uplink2100,
                FrequencyStart = 1920,
                FrequencyEnd = 1980,
                FcnStart = 18000,
                FcnEnd = 18600 }
        };

        public static FrequencyBandType GetBandFromFrequency(this double frequency)
        {
            var def = frequencyBands.FirstOrDefault(
                x => frequency >= x.FrequencyStart && frequency <= x.FrequencyEnd);

            return def?.FrequencyBandType ?? FrequencyBandType.Undefined;
        }

        public static FrequencyBandType GetBandFromFcn(this string frequency)
        {
            switch (frequency)
            {
                case "all":
                    return FrequencyBandType.All;
                case "2100":
                    return FrequencyBandType.Band2100;
                case "1800":
                    return FrequencyBandType.Band1800;
                case "college":
                    return FrequencyBandType.College;
                default:
                    return FrequencyBandType.Band800VoLte;
            }
        }

        public static bool IsCdmaFrequency(this short frequency)
        {
            return frequency == 37 || frequency == 78 || frequency == 119 || frequency == 160
                || frequency == 201 || frequency == 242 || frequency == 283 || frequency == 1013;
        }
    }
}
