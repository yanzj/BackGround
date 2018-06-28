using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;

namespace Lte.Domain.Common
{
    public class NeighborCellHwCsv
    {
        [CsvColumn(Name = "邻小区")]
        public string CellRelation { get; set; }

        [CsvColumn(Name = "特定两小区间切换出尝试次数 (无)")]
        public int TotalTimes { get; set; }

        public static List<NeighborCellHwCsv> ReadNeighborCellHwCsvs(StreamReader reader)
        {
            var infos = CsvContext.Read<NeighborCellHwCsv>(reader, CsvFileDescription.CommaDescription);
            var groupInfos = (from info in infos
                group info by info.CellRelation
                into g
                select new NeighborCellHwCsv
                {
                    CellRelation = g.Key,
                    TotalTimes = g.Sum(x => x.TotalTimes)
                }).ToList();
            return groupInfos;
        }
    }
}