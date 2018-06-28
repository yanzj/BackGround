using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class WorkItemExcel
    {
        [ExcelColumn("故障单号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("故障标题")]
        public string Title { get; set; }

        public string SubTypeDescription
            => Title.Replace("集团接口", "").Replace("省接口", "").Replace("【移动业务感知/无线】", "").GetSplittedFields("/Ne=")[0];

        [ExcelColumn("故障种类")]
        public string TypeDescription { get; set; }

        [ExcelColumn("故障位置")]
        public string Position { get; set; }

        public string[] NetworkElement
            => Position.Contains(':') ? Position.GetSplittedFields(':')[1].GetSplittedFields('_') : new[] { "" };

        public string ENodebPart => SplittedContents.FirstOrDefault(x => x.StartsWith("enb_id："));

        public int ENodebId
            =>
                NetworkElement.Length > 2
                    ? NetworkElement[1].ConvertToInt(0)
                    : ENodebPart?.Replace("enb_id：", "").ConvertToInt(0) ?? 0;

        public string SectorPart => SplittedContents.FirstOrDefault(x => x.StartsWith("cell_id："));

        public byte SectorId
            =>
                NetworkElement.Length > 2
                    ? NetworkElement[2].ConvertToByte(0)
                    : SectorPart?.Replace("cell_id：", "").ConvertToByte(0) ?? 0;

        [ExcelColumn("建单时间")]
        public DateTime BeginTime { get; set; }

        [ExcelColumn("应恢复时间")]
        public DateTime Deadline { get; set; }

        [ExcelColumn("受理部门")]
        public string StaffName { get; set; }

        [ExcelColumn("故障修复时间")]
        public DateTime? FinishTime { get; set; }

        [ExcelColumn("故障状态")]
        public string StateDescription { get; set; }

        [ExcelColumn("故障原因")]
        public string MalfunctionCause { get; set; }

        public string CauseDescription
            => string.IsNullOrEmpty(MalfunctionCause) ? "" : MalfunctionCause.GetSplittedFields(',')[0];

        [ExcelColumn("故障内容")]
        public string Contents { get; set; }

        public string[] SplittedContents => Contents.GetSplittedFields("；");

        public string[] Information => Contents.GetSplittedFields("<br/>");

        public string DateTimeString
            =>
                Information.FirstOrDefault(x => x.StartsWith("【告警附件文本信息】:"))?.Replace("【告警附件文本信息】:", "") ??
                (Information.Length > 0 ? Information[0] : DateTime.Today.ToShortDateString());

        public IEnumerable<string> Condition => Information.Where(x => x.StartsWith("问题判决条件:") || x.Contains("请求次数："));

        public string Comments
            => "[" + DateTimeString + "]" + (Condition.Any() ? Condition.Aggregate((x, y) => x + y) : "");
    }
}