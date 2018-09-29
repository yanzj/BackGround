using Lte.Domain.Common.Types;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public class RssiHuaweiCsv : IStatTime
    {
        [CsvColumn(Name = "开始时间")]
        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "小区")]
        [ArraySumProtection]
        public string CellInfo { get; set; }

        public int ENodebId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 4 ? 0 : fields[3].GetSplittedFields('=')[1].ConvertToInt(0);
            }
        }

        public byte LocalCellId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 4 ? (byte)0 : fields[1].GetSplittedFields('=')[1].ConvertToByte(0);
            }
        }

        public byte SectorId
        {
            get
            {
                if (string.IsNullOrEmpty(CellInfo)) return 0;
                var fields = CellInfo.GetSplittedFields(", ");
                return fields.Length < 6 ? (byte)0 : fields[5].GetSplittedFields('=')[1].ConvertToByte(0);
            }
        }

        [CsvColumn(Name = "平均RSSI≤-120dBm (无)")]
        public long AverageRssiBelow120 { get; set; }

        [CsvColumn(Name = "-120dBm＜平均RSSI≤-100dBm (无)")]
        public long AverageRssi120To100 { get; set; }

        [CsvColumn(Name = "-100dBm＜平均RSSI≤-92dBm (无)")]
        public long AverageRssi100To92 { get; set; }

        [CsvColumn(Name = "-92dBm＜平均RSSI≤-80dBm (无)")]
        public long AverageRssi92To80 { get; set; }

        [CsvColumn(Name = "平均RSSI＞-80dBm (无)")]
        public long AverageRssiAbove80 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index0的次数 (无)")]
        public long PucchRssiIndex0 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index1的次数 (无)")]
        public long PucchRssiIndex1 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index2的次数 (无)")]
        public long PucchRssiIndex2 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index3的次数 (无)")]
        public long PucchRssiIndex3 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index4的次数 (无)")]
        public long PucchRssiIndex4 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index5的次数 (无)")]
        public long PucchRssiIndex5 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index6的次数 (无)")]
        public long PucchRssiIndex6 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index7的次数 (无)")]
        public long PucchRssiIndex7 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index8的次数 (无)")]
        public long PucchRssiIndex8 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index9的次数 (无)")]
        public long PucchRssiIndex9 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index10的次数 (无)")]
        public long PucchRssiIndex10 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index11的次数 (无)")]
        public long PucchRssiIndex11 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index12的次数 (无)")]
        public long PucchRssiIndex12 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index13的次数 (无)")]
        public long PucchRssiIndex13 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index14的次数 (无)")]
        public long PucchRssiIndex14 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index15的次数 (无)")]
        public long PucchRssiIndex15 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index16的次数 (无)")]
        public long PucchRssiIndex16 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index17的次数 (无)")]
        public long PucchRssiIndex17 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index18的次数 (无)")]
        public long PucchRssiIndex18 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index19的次数 (无)")]
        public long PucchRssiIndex19 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index20的次数 (无)")]
        public long PucchRssiIndex20 { get; set; }

        [CsvColumn(Name = "PUCCH上检测到PRB级别的平均RSSI为Index21的次数 (无)")]
        public long PucchRssiIndex21 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index0的次数 (无)")]
        public long PuschRssiIndex0 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index1的次数 (无)")]
        public long PuschRssiIndex1 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index2的次数 (无)")]
        public long PuschRssiIndex2 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index3的次数 (无)")]
        public long PuschRssiIndex3 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index4的次数 (无)")]
        public long PuschRssiIndex4 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index5的次数 (无)")]
        public long PuschRssiIndex5 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index6的次数 (无)")]
        public long PuschRssiIndex6 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index7的次数 (无)")]
        public long PuschRssiIndex7 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index8的次数 (无)")]
        public long PuschRssiIndex8 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index9的次数 (无)")]
        public long PuschRssiIndex9 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index10的次数 (无)")]
        public long PuschRssiIndex10 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index11的次数 (无)")]
        public long PuschRssiIndex11 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index12的次数 (无)")]
        public long PuschRssiIndex12 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index13的次数 (无)")]
        public long PuschRssiIndex13 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index14的次数 (无)")]
        public long PuschRssiIndex14 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index15的次数 (无)")]
        public long PuschRssiIndex15 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index16的次数 (无)")]
        public long PuschRssiIndex16 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index17的次数 (无)")]
        public long PuschRssiIndex17 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index18的次数 (无)")]
        public long PuschRssiIndex18 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index19的次数 (无)")]
        public long PuschRssiIndex19 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index20的次数 (无)")]
        public long PuschRssiIndex20 { get; set; }

        [CsvColumn(Name = "PUSCH上检测到PRB级别的平均RSSI为Index21的次数 (无)")]
        public long PuschRssiIndex21 { get; set; }

        [CsvColumn(Name = "系统上行每个PRB上检测到的干扰噪声的最大值 (毫瓦分贝)")]
        public string MaxRssiRbString { get; set; }

        [CsvColumn(Name = "系统上行每个PRB上检测到的干扰噪声的最小值 (毫瓦分贝)")]
        public string MinRssiRbString { get; set; }

        [CsvColumn(Name = "系统上行每个PRB上检测到的干扰噪声的平均值 (毫瓦分贝)")]
        public string AverageRssiRbString { get; set; }

        public static List<RssiHuaweiCsv> ReadRssiHuaweiCsvs(StreamReader reader)
        {
            return CsvContext.Read<RssiHuaweiCsv>(reader, CsvFileDescription.CommaDescription).ToList();
        }
    }
}
