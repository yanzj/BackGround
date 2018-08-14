using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Cdma
{
    [TypeDoc("������ͼ�����ڵ�������ʾ")]
    [AutoMapFrom(typeof(CdmaCellView))]
    public class CdmaSectorView
    {
        [MemberDoc("С�����ƣ����ڱ���С��")]
        public string CellName { get; set; }

        [MemberDoc("��վ���")]
        public int BtsId { get; set; }

        [MemberDoc("�������")]
        public byte SectorId { get; set; }

        [MemberDoc("�Ƿ�Ϊ����С��")]
        public string Indoor { get; set; }

        [MemberDoc("��λ��")]
        public double Azimuth { get; set; }

        [MemberDoc("����")]
        public double Longtitute { get; set; }

        [MemberDoc("γ��")]
        public double Lattitute { get; set; }

        [MemberDoc("���߹Ҹ�")]
        public double Height { get; set; }

        [MemberDoc("�����")]
        public double DownTilt { get; set; }

        [MemberDoc("��������")]
        public double AntennaGain { get; set; }

        [MemberDoc("Ƶ��")]
        public int Frequency { get; set; }

        public short Pn { get; set; }

        public string CellType { get; set; }

        [MemberDoc("������Ϣ")]
        public string OtherInfos { get; set; }
    }
}