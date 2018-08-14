using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [TypeDoc("����LTEС�����ݿ��ж�Ӧ��ORM����")]
    [AutoMapFrom(typeof(CellExcel))]
    public class Cell : Entity, ILteCellParameters<short, int>, ILteCellQuery, IGeoPoint<double>, ICellAntenna<double>
    {
        [MemberDoc("��վ���")]
        public int ENodebId { get; set; }

        [MemberDoc("������ţ�Ŀǰ2.1Gȡ0-15��1.8Gȡ48-63��800Mȡ16-31")]
        public byte SectorId { get; set; }

        [MemberDoc("����С�����")]
        public byte LocalSectorId { get; set; }

        [MemberDoc("Ƶ��ţ�Ŀǰ2.1G��Ҫ��100Ƶ�㣬1.8G��Ҫ��1825Ƶ�㣬800M��Ҫ��2446Ƶ��")]
        public int Frequency { get; set; }

        [MemberDoc("Ƶ����ţ���1��ʾ2.1G��3��ʾ1.8G��5��ʾ800M")]
        public byte BandClass { get; set; }

        [MemberDoc("����С����ţ���Χ0-503")]
        public short Pci { get; set; }

        [MemberDoc("������������ŵ�������������Χ0-839")]
        public short Prach { get; set; }

        [MemberDoc("�ο��ź�(Reference Signal)���ʣ���λ��dBm")]
        public double RsPower { get; set; }

        [AutoMapPropertyResolve("IsIndoor", typeof(CellExcel), typeof(IndoorDescriptionToOutdoorBoolTransform))]
        [MemberDoc("�Ƿ�Ϊ����С��")]
        public bool IsOutdoor { get; set; }

        [MemberDoc("���������")]
        public int Tac { get; set; }

        [MemberDoc("С������")]
        public double Longtitute { get; set; }

        [MemberDoc("С��γ��")]
        public double Lattitute { get; set; }

        [MemberDoc("���߹Ҹߣ���λ��")]
        public double Height { get; set; }

        [MemberDoc("��λ��")]
        public double Azimuth { get; set; }

        [MemberDoc("��е����")]
        public double MTilt { get; set; }

        [MemberDoc("��������")]
        public double ETilt { get; set; }

        [MemberDoc("�������棬��λ��dB")]
        public double AntennaGain { get; set; }

        [AutoMapPropertyResolve("TransmitReceive", typeof(CellExcel), typeof(AntennaPortsConfigureTransform))]
        [MemberDoc("�����շ�����")]
        public AntennaPortsConfigure AntennaPorts { get; set; }

        [MemberDoc("�Ƿ�����")]
        public bool IsInUse { get; set; } = true;
    }
}