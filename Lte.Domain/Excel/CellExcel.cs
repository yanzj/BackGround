using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("定义记录LTE小区的信息的Excel导出数据项")]
    public class CellExcel : ILteCellParameters<short, int>, ICellAntenna<double>
    {
        [ExcelColumn("CELL_ID", TransformEnum.ByteRemoveQuotions)]
        public byte SectorId { get; set; }

        [ExcelColumn("FSL编号")]
        public string PlanNum { get; set; }

        [ExcelColumn("SectorID")]
        public byte LocalSectorId { get; set; }

        [ExcelColumn("频点")]
        public int Frequency { get; set; }

        [ExcelColumn("拉远名称")]
        public string RruName { get; set; }

        [ExcelColumn("是否接室分")]
        public string IsIndoor { get; set; } = "否";

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("天线挂高")]
        public double Height { get; set; }

        [ExcelColumn("机械下倾角")]
        public double MTilt { get; set; }

        [ExcelColumn("电下倾角")]
        public double ETilt { get; set; }

        [ExcelColumn("方位角")]
        public double Azimuth { get; set; }

        [ExcelColumn("天线增益")]
        public double AntennaGain { get; set; }

        [ExcelColumn("eNodeB ID", TransformEnum.IntegerRemoveQuotions)]
        public int ENodebId { get; set; }

        [ExcelColumn("频段号")]
        public byte BandClass { get; set; }

        [ExcelColumn("天线信息")]
        public string AntennaInfo { get; set; }

        [ExcelColumn("收发类型")]
        public string TransmitReceive { get; set; }

        [ExcelColumn("共天线信息")]
        public string ShareCdmaInfo { get; set; }

        [ExcelColumn("PCI")]
        public short Pci { get; set; }

        [ExcelColumn("根序列索引")]
        public short Prach { get; set; }

        [ExcelColumn("TAC")]
        public int Tac { get; set; }

        [ExcelColumn("参考信号功率")]
        public double RsPower { get; set; }

        [ExcelColumn("C网共站小区ID")]
        public string CdmaCellId { get; set; }

        [ExcelColumn("天线厂家")]
        public string AntennaFactoryString { get; set; }

        [ExcelColumn("天线型号")]
        public string AntennaModel { get; set; }

        [ExcelColumn("能否电调")]
        public string CanBeETiltDescription { get; set; }

        [ExcelColumn("是否美化")]
        public string IsBeautifyDescription { get; set; }

        [ExcelColumn("是否CA")]
        public string IsCaDescription { get; set; }
    }
}