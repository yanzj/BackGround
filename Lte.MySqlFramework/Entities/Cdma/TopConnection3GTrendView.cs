using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Cdma
{
    [AutoMapFrom(typeof(TopConnection3GTrend))]
    public class TopConnection3GTrendView
    {
        [MemberDoc("С������")]
        public string CellName { get; set; }

        [MemberDoc("LTE��վ����")]
        public string ENodebName { get; set; }

        [MemberDoc("С�����")]
        public int CellId { get; set; }

        [MemberDoc("CDMA��վ����")]
        public string CdmaName { get; set; }

        [MemberDoc("LTE��վ����")]
        public string LteName { get; set; }

        public int BtsId { get; set; }

        [MemberDoc("�������")]
        public byte SectorId { get; set; }

        [MemberDoc("���ߵ��ߴ���")]
        public int WirelessDrop { get; set; }

        [MemberDoc("���ӳ��Դ���")]
        public int ConnectionAttempts { get; set; }

        [MemberDoc("����ʧ�ܴ���")]
        public int ConnectionFails { get; set; }

        [MemberDoc("��·��æ��")]
        public double LinkBusyRate { get; set; }

        [MemberDoc("���ӳɹ���")]
        public double ConnectionRate => (double)(ConnectionAttempts - ConnectionFails) / ConnectionAttempts;

        [MemberDoc("������")]
        public double DropRate => (double)WirelessDrop / (ConnectionAttempts - ConnectionFails);

        [MemberDoc("����TOP��������")]
        public int TopDates { get; set; }
    }
}