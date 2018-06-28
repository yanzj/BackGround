using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Types;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(CdmaCell))]
    public class CdmaCellView
    {
        public string BtsName { get; set; }

        public int BtsId { get; set; } = -1;

        public byte SectorId { get; set; } = 31;

        public string CellName => BtsName + "-" + SectorId;

        public string CellType { get; set; } = "DO";

        public int Frequency { get; set; } = 0;

        public int CellId { get; set; }

        public string Lac { get; set; }

        public short Pn { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double Height { get; set; }

        public double MTilt { get; set; }

        public double ETilt { get; set; }

        public double DownTilt => MTilt + ETilt;

        public double Azimuth { get; set; }

        public double AntennaGain { get; set; }

        [AutoMapPropertyResolve("IsOutdoor", typeof(CdmaCell), typeof(OutdoorDescriptionTransform))]
        public string Indoor { get; set; }

        public double RsPower { get; set; }

        public string FrequencyList { get; set; }

        public string OtherInfos => "Cell Type: " + CellType + "; Cell ID: " + CellId + "; LAC: " + Lac + "; PN: " +
                                    Pn + "; Frequency List: " + FrequencyList + "; BtsId: " + BtsId;
    }
}