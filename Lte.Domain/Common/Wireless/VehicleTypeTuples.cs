using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(VehicleType), CPlusL)]
    public enum VehicleType : short
    {
        CdmaHuawei,
        CdmaZte,
        CdmaAl,
        PhsUt,
        PhsZte,
        SatelliteC,
        SatelliteKu,
        Flyaway,
        Electirc1000Kw,
        Electirc200Kw,
        Electric60Kw,
        SoftSwitch,
        LittleYouji,
        LittleMicrowave,
        MarineVstat,
        EmergencyVstat,
        Broadcast,
        CPlusL,
        LteHuawei,
        LteZte,
        LteEricsson
    }

    public class VehicularTypeDescriptionTransform : DescriptionTransform<VehicleType>
    {

    }

    public class VehicularTypeTransform : EnumTransform<VehicleType>
    {
        public VehicularTypeTransform() : base(VehicleType.CPlusL)
        {
        }
    }

    internal static class VehicleTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(VehicleType.CdmaHuawei, "C网华为"),
                new Tuple<object, string>(VehicleType.CdmaZte, "C网中兴"),
                new Tuple<object, string>(VehicleType.CdmaAl, "C网阿朗"),
                new Tuple<object, string>(VehicleType.PhsUt, "PHS UTStarcom"),
                new Tuple<object, string>(VehicleType.PhsZte, "PHS中兴"),
                new Tuple<object, string>(VehicleType.SatelliteC, "PHS中兴"),
                new Tuple<object, string>(VehicleType.SatelliteKu, "Ku频段卫星车"),
                new Tuple<object, string>(VehicleType.Flyaway, "Flyaway"),
                new Tuple<object, string>(VehicleType.Electirc1000Kw, "1000KW电源车"),
                new Tuple<object, string>(VehicleType.Electirc200Kw, "200KW电源车"),
                new Tuple<object, string>(VehicleType.Electric60Kw, "60KW电源车"),
                new Tuple<object, string>(VehicleType.SoftSwitch, "软交换应急车"),
                new Tuple<object, string>(VehicleType.LittleYouji, "小油机"),
                new Tuple<object, string>(VehicleType.LittleMicrowave, "小微波"),
                new Tuple<object, string>(VehicleType.MarineVstat, "海事卫星电话"),
                new Tuple<object, string>(VehicleType.EmergencyVstat, "应急VSAT"),
                new Tuple<object, string>(VehicleType.Broadcast, "电台"),
                new Tuple<object, string>(VehicleType.CPlusL, "C+L"),
                new Tuple<object, string>(VehicleType.LteHuawei, "L网华为"),
                new Tuple<object, string>(VehicleType.LteZte, "L网中兴"),
                new Tuple<object, string>(VehicleType.LteEricsson, "L网爱立信")
            };
        }
    }
}
