using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;

namespace Lte.Domain.Common.Wireless.Kpi
{
    public class HourCqiCsv: IStatTime, ILteCellQuery
    {
        [CsvColumn(Name = "时间")]
        public DateTime StatTime { get; set; }
        
        [CsvColumn(Name = "ENBID")]
        public int ENodebId { get; set; }
        
        [CsvColumn(Name = "小区ID")]
        public byte SectorId { get; set; }
        
        [CsvColumn(Name = "小区标识")]
        public string CellSerialNumber { get; set; }

        [CsvColumn(Name = "12.2 CQI0上报数量(次)")]
        public int Cqi0Times { get; set; }
        
        [CsvColumn(Name = "12.2 CQI1上报数量(次)")]
        public int Cqi1Times { get; set; }

        [CsvColumn(Name = "12.2 CQI2上报数量(次)")]
        public int Cqi2Times { get; set; }

        [CsvColumn(Name = "12.2 CQI3上报数量(次)")]
        public int Cqi3Times { get; set; }

        [CsvColumn(Name = "12.2 CQI4上报数量(次)")]
        public int Cqi4Times { get; set; }

        [CsvColumn(Name = "12.2 CQI5上报数量(次)")]
        public int Cqi5Times { get; set; }

        [CsvColumn(Name = "12.2 CQI6上报数量(次)")]
        public int Cqi6Times { get; set; }

        [CsvColumn(Name = "12.2 CQI7上报数量(次)")]
        public int Cqi7Times { get; set; }

        [CsvColumn(Name = "12.2 CQI8上报数量(次)")]
        public int Cqi8Times { get; set; }

        [CsvColumn(Name = "12.2 CQI9上报数量(次)")]
        public int Cqi9Times { get; set; }

        [CsvColumn(Name = "12.2 CQI10上报数量(次)")]
        public int Cqi10Times { get; set; }

        [CsvColumn(Name = "12.2 CQI11上报数量(次)")]
        public int Cqi11Times { get; set; }

        [CsvColumn(Name = "12.2 CQI12上报数量(次)")]
        public int Cqi12Times { get; set; }

        [CsvColumn(Name = "12.2 CQI13上报数量(次)")]
        public int Cqi13Times { get; set; }

        [CsvColumn(Name = "12.2 CQI14上报数量(次)")]
        public int Cqi14Times { get; set; }

        [CsvColumn(Name = "12.2 CQI15上报数量(次)")]
        public int Cqi15Times { get; set; }
        
        [CsvColumn(Name = "12.1 下行实际占用的PRB总数(个)")]
        public double TotalPrbs { get; set; }

        [CsvColumn(Name = "12.1 下行双流模式调度的PRB总数(个)")]
        public long DoubleFlowPrbs { get; set; }
        
        public static List<HourCqiCsv> ReadCsvs(StreamReader reader)
        {
            return
                CsvContext.Read<HourCqiCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList();
        }
    }
}
