using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(CoverageHotspot), CoverageHotspot.Others)]
    public enum CoverageHotspot : byte
    {
        College,
        Scenic,
        TransportJunction,
        RuralMarket,
        Exhibition,
        Stadium,
        ComercialCenter,
        LargeEnterprise,
        Hospital,
        Harbour,
        Residence,
        BusinessHall,
        Hotel,
        OfficeBuilding,
        Cemetery,
        Sea,
        None,
        Others
    }

    public class CoverageHotspotDescriptionTransform : DescriptionTransform<CoverageHotspot>
    {

    }

    public class CoverageHotspotTransform : EnumTransform<CoverageHotspot>
    {
        public CoverageHotspotTransform() : base(CoverageHotspot.Others)
        {
        }
    }

    internal static class CoverageHotspotTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CoverageHotspot.College, "校园"),
                new Tuple<object, string>(CoverageHotspot.Scenic, "风景区"),
                new Tuple<object, string>(CoverageHotspot.TransportJunction, "交通枢纽"),
                new Tuple<object, string>(CoverageHotspot.RuralMarket, "集贸市场"),
                new Tuple<object, string>(CoverageHotspot.Exhibition, "会展中心"),
                new Tuple<object, string>(CoverageHotspot.Stadium, "体育场"),
                new Tuple<object, string>(CoverageHotspot.ComercialCenter, "商业中心"),
                new Tuple<object, string>(CoverageHotspot.LargeEnterprise, "大型企业"),
                new Tuple<object, string>(CoverageHotspot.Hospital, "医院"),
                new Tuple<object, string>(CoverageHotspot.Harbour, "港口"),
                new Tuple<object, string>(CoverageHotspot.Residence, "住宅小区"),
                new Tuple<object, string>(CoverageHotspot.BusinessHall, "营业厅"),
                new Tuple<object, string>(CoverageHotspot.Hotel, "星级宾馆"),
                new Tuple<object, string>(CoverageHotspot.OfficeBuilding, "办公楼宇"),
                new Tuple<object, string>(CoverageHotspot.Cemetery, "公墓"),
                new Tuple<object, string>(CoverageHotspot.Sea, "海域"),
                new Tuple<object, string>(CoverageHotspot.None, "无")
            };
        }
    }
}
