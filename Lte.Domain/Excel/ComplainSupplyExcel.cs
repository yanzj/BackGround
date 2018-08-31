using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class ComplainSupplyExcel : IDistrictTown, IGeoPoint<double>
    {
        [ExcelColumn("工单编号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("区域")]
        public string District { get; set; }

        [ExcelColumn("镇区")]
        public string Town { get; set; }

        [ExcelColumn("今年重复投诉次数")]
        public byte RepeatTimes { get; set; }

        [ExcelColumn("地址")]
        public string BuildingName { get; set; }

        [ExcelColumn("投诉点经度", TransformEnum.Longtitute, 0)]
        public double Longtitute { get; set; }

        [ExcelColumn("投诉点纬度", TransformEnum.Lattitute, 0)]
        public double Lattitute { get; set; }

        [ExcelColumn("使用场合")]
        public string Scene { get; set; }

        [ExcelColumn("用户名称")]
        public string SubscriberInfo { get; set; }

        [ExcelColumn("申告号码")]
        public string SubscriberPhone { get; set; }

        [ExcelColumn("联系电话")]
        public string ContactPhone { get; set; }

        [ExcelColumn("申告内容")]
        public string ComplainContents { get; set; }

        [ExcelColumn("投诉表象")]
        public string CategoryDescription { get; set; }

        [ExcelColumn("业务类型")]
        public string NetworkDescription { get; set; }

        [ExcelColumn("室内外选择")]
        public string IndoorDescription { get; set; }

        [ExcelColumn("回单内容")]
        public string FinishContents { get; set; }

        [ExcelColumn("原因定位")]
        public string CauseLocation { get; set; }

        [ExcelColumn("规划站点名称")]
        public string SitePosition { get; set; }

        [ExcelColumn("是否已解决")]
        public string CurrentStateDescription { get; set; }

        [ExcelColumn("受理人班组")]
        public string CurrentProcessor { get; set; }

        [ExcelColumn("工单来源")]
        public string SourceDescription { get; set; }

        [ExcelColumn("是否为百度经纬度")]
        public string BaiduOffsetDescription { get; set; }

    }
}
