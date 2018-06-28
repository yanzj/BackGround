using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.LinqToCsv;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(MrGridKpi))]
    public class MrGridKpiDto : IGeoGridPoint<double>
    {
        [CsvColumn(Name = "X")]
        public int X { get; set; }

        [CsvColumn(Name = "Y")]
        public int Y { get; set; }

        public double Longtitute => 112 + X*0.00049;

        public double Lattitute => 22 + Y*0.00045;

        [CsvColumn(Name = "MR总数")]
        public int MrCount { get; set; }

        [CsvColumn(Name = "弱覆盖数")]
        public int WeakCount { get; set; }

        public double WeakCoverageRate => MrCount == 0 ? 0 : (double) WeakCount/MrCount*100;

        [CsvColumn(Name = "平均RSRP")]
        public double Rsrp { get; set; }

        [CsvColumn(Name = "MR总数归一")]
        public int MrCountNormalize { get; set; }

        [CsvColumn(Name = "弱覆盖数归一")]
        public int WeakCountNormalize { get; set; }

        [CsvColumn(Name = "平均RSRP归一")]
        public int RsrpNormalize { get; set; }

        [CsvColumn(Name = "最近距离")]
        public int ShortestDistance { get; set; }
    }
}