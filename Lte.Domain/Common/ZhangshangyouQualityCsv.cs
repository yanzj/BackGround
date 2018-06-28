using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;

namespace Lte.Domain.Common
{
    public class ZhangshangyouQualityCsv : IStatTime
    {
        [CsvColumn(Name = "任务编号")]
        public string SerialNumber { get; set; }

        [CsvColumn(Name = "时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "回传网络")]
        public string BackhaulNetworkDescription { get; set; }

        [CsvColumn(Name = "任务类型")]
        public string TestTypeDescription { get; set; }
    }
}
