using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Cdma
{
    [TypeDoc("TOP����С��ָ��������ͼ")]
    [AutoMapFrom(typeof(TopDrop2GTrend))]
    public class TopDrop2GTrendView
    {
        [MemberDoc("С������")]
        public string CellName { get; set; }

        [MemberDoc("LTE��վ����")]
        public string ENodebName { get; set; }

        public int BtsId { get; set; }

        public byte SectorId { get; set; }

        public int CellId { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

        [MemberDoc("��������")]
        public int TotalDrops { get; set; }

        [MemberDoc("���г�������")]
        public int TotalCallAttempst { get; set; }

        [MemberDoc("������")]
        public double DropRate => TotalCallAttempst == 0 ? 0 : (double)TotalDrops / TotalCallAttempst;

        [MemberDoc("����TOP��������")]
        public int TopDates { get; set; }
    }
}