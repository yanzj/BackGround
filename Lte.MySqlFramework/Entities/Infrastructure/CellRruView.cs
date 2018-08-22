using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(Cell), typeof(LteRru))]
    [TypeDoc("LTE-RRUС����ͼ����ʾС��������RRU���ƣ���ϸ��Ϣ")]
    public class CellRruView
    {
        [MemberDoc("��վ����")]
        public string ENodebName { get; set; }

        [MemberDoc("��վ���")]
        public int ENodebId { get; set; }

        [MemberDoc("������ţ�Ŀǰ2.1Gȡ0-15��1.8Gȡ48-63��800Mȡ16-31")]
        public byte SectorId { get; set; }

        [MemberDoc("С������")]
        public string CellName => ENodebName + "-" + SectorId;

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

        [MemberDoc("���������")]
        public int Tac { get; set; }

        [MemberDoc("���߹Ҹߣ���λ��")]
        public double Height { get; set; }

        [MemberDoc("��λ��")]
        public double Azimuth { get; set; }

        [AutoMapPropertyResolve("IsOutdoor", typeof(Cell), typeof(OutdoorDescriptionTransform))]
        [MemberDoc("������С��")]
        public string Indoor { get; set; }

        [MemberDoc("��е����")]
        public double MTilt { get; set; }

        [MemberDoc("��������")]
        public double ETilt { get; set; }

        [MemberDoc("�������")]
        public double DownTilt => MTilt + ETilt;

        [MemberDoc("�������棬��λ��dB")]
        public double AntennaGain { get; set; }

        [MemberDoc("С������")]
        public double Longtitute { get; set; }

        [MemberDoc("С��γ��")]
        public double Lattitute { get; set; }

        [MemberDoc("������Ϣ")]
        public string OtherInfos
            => "PCI: " + Pci + "; PRACH: " + Prach + "; RS Power(dBm): " + RsPower + "; TAC: " +
               Tac + "; ENodebId: " + ENodebId;

        [MemberDoc("����С����ţ�һ���ǻ�Ϊ��Ч")]
        public byte LocalSectorId { get; set; }

        [MemberDoc("RRU����")]
        public string RruName { get; set; }

        [MemberDoc("������Ϣ")]
        public string AntennaInfo { get; set; }

        [MemberDoc("���߳���")]
        [AutoMapPropertyResolve("AntennaFactory", typeof(LteRru), typeof(AntennaFactoryDescriptionTransform))]
        public string AntennaFactoryDescription { get; set; }

        [MemberDoc("�����ͺ�")]
        public string AntennaModel { get; set; }

        [MemberDoc("�Ƿ�����")]
        public bool IsInUse { get; set; } = true;
    }
}