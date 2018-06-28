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
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common
{
    public class CqiHuaweiCsv : IStatTime
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

        [CsvColumn(Name = "单码字全带宽非周期CQI为0的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为1的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为2的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为3的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为4的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为5的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为6的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为7的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为8的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为9的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为10的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为11的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为12的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为13的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为14的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "单码字全带宽非周期CQI为15的上报次数 (无)")]
        public int SingleCodeFullBandInPeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为0的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为1的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为2的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为3的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为4的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为5的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为6的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为7的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为8的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为9的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为10的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为11的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为12的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为13的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为14的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "单码字全带宽周期CQI为15的上报次数 (无)")]
        public int SingleCodeFullBandPeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为0的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为1的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为2的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为3的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为4的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为5的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为6的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为7的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为8的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为9的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为10的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为11的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为12的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为13的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为14的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0非周期CQI为15的上报次数 (无)")]
        public int DoubleCodeSubCode0InPeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为0的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为1的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为2的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为3的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为4的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为5的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为6的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为7的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为8的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为9的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为10的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为11的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为12的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为13的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为14的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字0周期CQI为15的上报次数 (无)")]
        public int DoubleCodeSubCode0PeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为0的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为1的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为2的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为3的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为4的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为5的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为6的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为7的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为8的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为9的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为10的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为11的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为12的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为13的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为14的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1非周期CQI为15的上报次数 (无)")]
        public int DoubleCodeSubCode1InPeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为0的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi0Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为1的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi1Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为2的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi2Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为3的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi3Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为4的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi4Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为5的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi5Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为6的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi6Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为7的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi7Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为8的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi8Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为9的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi9Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为10的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi10Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为11的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi11Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为12的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi12Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为13的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi13Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为14的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi14Times { get; set; }

        [CsvColumn(Name = "双码字全带宽码字1周期CQI为15的上报次数 (无)")]
        public int DoubleCodeSubCode1PeriodicCqi15Times { get; set; }

        [CsvColumn(Name = "下行闭环MIMO RANK为1的PRB累加个数 (无)")]
        public long CloseLoopRank1Prbs { get; set; }

        [CsvColumn(Name = "下行闭环MIMO RANK为2的PRB累加个数 (无)")]
        public long CloseLoopRank2Prbs { get; set; }

        [CsvColumn(Name = "下行开环MIMO RANK为1的PRB累加个数 (无)")]
        public long OpenLoopRank1Prbs { get; set; }

        [CsvColumn(Name = "下行开环MIMO RANK为2的PRB累加个数 (无)")]
        public long OpenLoopRank2Prbs { get; set; }

        public static List<CqiHuaweiCsv> ReadFlowHuaweiCsvs(StreamReader reader)
        {
            return CsvContext.Read<CqiHuaweiCsv>(reader, CsvFileDescription.CommaDescription).ToList();
        }
    }
}
