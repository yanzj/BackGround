using System;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class BranchDemandExcel : IDistrictTown
    {
        [ExcelColumn("�û����ʱ��")]
        public DateTime BeginDate { get; set; }

        [ExcelColumn("���")]
        public string SerialNumber { get; set; }

        [ExcelColumn("��������")]
        public string District { get; set; }

        [ExcelColumn("������")]
        public string Town { get; set; }

        [ExcelColumn("�û������������")]
        public string ComplainContents { get; set; }

        [ExcelColumn("�������")]
        public string FirstContents { get; set; }

        [ExcelColumn("�����ʽ�������ʩ���ࣩ")]
        public string SolveFunctionDescription { get; set; }

        [ExcelColumn("�����Ƿ���")]
        public string IsSolvedDescription { get; set; }

        [ExcelColumn("���ʱ��")]
        public DateTime? EndDate { get; set; }

        [ExcelColumn("����")]
        public double Lontitute { get; set; }

        [ExcelColumn("γ��")]
        public double Lattitute { get; set; }

        [ExcelColumn("�û��������绰����")]
        public string SubscriberInfo { get; set; }

        [ExcelColumn("�ͻ���������")]
        public string ManagerInfo { get; set; }
    }
}