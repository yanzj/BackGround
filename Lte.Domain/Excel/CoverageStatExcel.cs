using System;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class CoverageStatExcel: ILteCellQuery
    {
        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        [ExcelColumn("基站id")]
        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ExcelColumn("小区id")]
        [ArraySumProtection]
        public byte SectorId { get; set; }

        [ExcelColumn("AGPS-MR覆盖最优记录数")]
        public string AgpsCoverageMrsString { get; set; }

        public int AgpsCoverageMrs => AgpsCoverageMrsString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优RSRP均值")]
        public string AgpsCoverageRateString { get; set; }

        public double AgpsCoverageSum => AgpsCoverageRateString.ConvertToDouble(0) * AgpsCoverageMrs;

        [ExcelColumn("AGPS-MR覆盖最优95记录数")]
        public string AgpsCoverageAbove95String { get; set; }

        public int AgpsCoverageAbove95 => AgpsCoverageAbove95String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优100记录数")]
        public string AgpsCoverageAbove100String { get; set; }

        public int AgpsCoverageAbove100 => AgpsCoverageAbove100String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优105记录数")]
        public string AgpsCoverageAbove105String { get; set; }

        public int AgpsCoverageAbove105 => AgpsCoverageAbove105String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优110记录数")]
        public string AgpsCoverageAbove110String { get; set; }

        public int AgpsCoverageAbove110 => AgpsCoverageAbove110String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优115记录数")]
        public string AgpsCoverageAbove115String { get; set; }

        public int AgpsCoverageAbove115 => AgpsCoverageAbove115String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优弱栅格数")]
        public string AgpsCoverageWeakString { get; set; }

        public int AgpsCoverageWeak => AgpsCoverageWeakString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR覆盖最优总栅格数")]
        public string AgpsCoverageTotalString { get; set; }

        public int AgpsCoverageTotal => AgpsCoverageTotalString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优记录数")]
        public string AgpsTelecomMrsString { get; set; }

        public int AgpsTelecomMrs => AgpsTelecomMrsString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优RSRP均值")]
        public string AgpsTelecomRateString { get; set; }

        public double AgpsTelecomSum => AgpsTelecomRateString.ConvertToDouble(0) * AgpsTelecomMrs;

        [ExcelColumn("AGPS-MR规划最优95记录数")]
        public string AgpsTelecomAbove95String { get; set; }

        public int AgpsTelecomAbove95 => AgpsTelecomAbove95String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优100记录数")]
        public string AgpsTelecomAbove100String { get; set; }

        public int AgpsTelecomAbove100 => AgpsTelecomAbove100String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优105记录数")]
        public string AgpsTelecomAbove105String { get; set; }

        public int AgpsTelecomAbove105 => AgpsTelecomAbove105String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优110记录数")]
        public string AgpsTelecomAbove110String { get; set; }

        public int AgpsTelecomAbove110 => AgpsTelecomAbove110String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优115记录数")]
        public string AgpsTelecomAbove115String { get; set; }

        public int AgpsTelecomAbove115 => AgpsTelecomAbove115String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优弱栅格数")]
        public string AgpsTelecomWeakString { get; set; }

        public int AgpsTelecomWeak => AgpsTelecomWeakString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR规划最优总栅格数")]
        public string AgpsTelecomTotalString { get; set; }

        public int AgpsTelecomTotal => AgpsTelecomTotalString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入记录数")]
        public string AgpsAccessMrsString { get; set; }

        public int AgpsAccessMrs => AgpsAccessMrsString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入RSRP均值")]
        public string AgpsAccessRateString { get; set; }

        public double AgpsAccessSum => AgpsAccessRateString.ConvertToDouble(0) * AgpsAccessMrs;

        [ExcelColumn("AGPS-MR主接入95记录数")]
        public string AgpsAccessAbove95String { get; set; }

        public int AgpsAccessAbove95 => AgpsAccessAbove95String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入100记录数")]
        public string AgpsAccessAbove100String { get; set; }

        public int AgpsAccessAbove100 => AgpsAccessAbove100String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入105记录数")]
        public string AgpsAccessAbove105String { get; set; }

        public int AgpsAccessAbove105 => AgpsAccessAbove105String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入110记录数")]
        public string AgpsAccessAbove110String { get; set; }

        public int AgpsAccessAbove110 => AgpsAccessAbove110String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入115记录数")]
        public string AgpsAccessAbove115String { get; set; }

        public int AgpsAccessAbove115 => AgpsAccessAbove115String.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入弱栅格数")]
        public string AgpsAccessWeakString { get; set; }

        public int AgpsAccessWeak => AgpsAccessWeakString.ConvertToInt(0);

        [ExcelColumn("AGPS-MR主接入总栅格数")]
        public string AgpsAccessTotalString { get; set; }

        public int AgpsAccessTotal => AgpsAccessTotalString.ConvertToInt(0);

        //a

        [ExcelColumn("全量MR覆盖最优记录数")]
        public string CoverageMrsString { get; set; }

        public int CoverageMrs => CoverageMrsString.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优RSRP均值")]
        public string CoverageRateString { get; set; }

        public double CoverageSum => CoverageRateString.ConvertToDouble(0) * CoverageMrs;

        [ExcelColumn("全量MR覆盖最优95记录数")]
        public string CoverageAbove95String { get; set; }

        public int CoverageAbove95 => CoverageAbove95String.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优100记录数")]
        public string CoverageAbove100String { get; set; }

        public int CoverageAbove100 => CoverageAbove100String.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优105记录数")]
        public string CoverageAbove105String { get; set; }

        public int CoverageAbove105 => CoverageAbove105String.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优110记录数")]
        public string CoverageAbove110String { get; set; }

        public int CoverageAbove110 => CoverageAbove110String.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优115记录数")]
        public string CoverageAbove115String { get; set; }

        public int CoverageAbove115 => CoverageAbove115String.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优弱栅格数")]
        public string CoverageWeakString { get; set; }

        public int CoverageWeak => CoverageWeakString.ConvertToInt(0);

        [ExcelColumn("全量MR覆盖最优总栅格数")]
        public string CoverageTotalString { get; set; }

        public int CoverageTotal => CoverageTotalString.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优记录数")]
        public string TelecomMrsString { get; set; }

        public int TelecomMrs => TelecomMrsString.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优RSRP均值")]
        public string TelecomRateString { get; set; }

        public double TelecomSum => TelecomRateString.ConvertToDouble(0) * TelecomMrs;

        [ExcelColumn("全量MR规划最优95记录数")]
        public string TelecomAbove95String { get; set; }

        public int TelecomAbove95 => TelecomAbove95String.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优100记录数")]
        public string TelecomAbove100String { get; set; }

        public int TelecomAbove100 => TelecomAbove100String.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优105记录数")]
        public string TelecomAbove105String { get; set; }

        public int TelecomAbove105 => TelecomAbove105String.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优110记录数")]
        public string TelecomAbove110String { get; set; }

        public int TelecomAbove110 => TelecomAbove110String.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优115记录数")]
        public string TelecomAbove115String { get; set; }

        public int TelecomAbove115 => TelecomAbove115String.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优弱栅格数")]
        public string TelecomWeakString { get; set; }

        public int TelecomWeak => TelecomWeakString.ConvertToInt(0);

        [ExcelColumn("全量MR规划最优总栅格数")]
        public string TelecomTotalString { get; set; }

        public int TelecomTotal => TelecomTotalString.ConvertToInt(0);

        [ExcelColumn("全量MR主接入记录数")]
        public string AccessMrsString { get; set; }

        public int AccessMrs => AccessMrsString.ConvertToInt(0);

        [ExcelColumn("全量MR主接入RSRP均值")]
        public string AccessRateString { get; set; }

        public double AccessSum => AccessRateString.ConvertToDouble(0) * AccessMrs;

        [ExcelColumn("全量MR主接入95记录数")]
        public string AccessAbove95String { get; set; }

        public int AccessAbove95 => AccessAbove95String.ConvertToInt(0);

        [ExcelColumn("全量MR主接入100记录数")]
        public string AccessAbove100String { get; set; }

        public int AccessAbove100 => AccessAbove100String.ConvertToInt(0);

        [ExcelColumn("全量MR主接入105记录数")]
        public string AccessAbove105String { get; set; }

        public int AccessAbove105 => AccessAbove105String.ConvertToInt(0);

        [ExcelColumn("全量MR主接入110记录数")]
        public string AccessAbove110String { get; set; }

        public int AccessAbove110 => AccessAbove110String.ConvertToInt(0);

        [ExcelColumn("全量MR主接入115记录数")]
        public string AccessAbove115String { get; set; }

        public int AccessAbove115 => AccessAbove115String.ConvertToInt(0);

        [ExcelColumn("全量MR主接入弱栅格数")]
        public string AccessWeakString { get; set; }

        public int AccessWeak => AccessWeakString.ConvertToInt(0);

        [ExcelColumn("全量MR主接入总栅格数")]
        public string AccessTotalString { get; set; }

        public int AccessTotal => AccessTotalString.ConvertToInt(0);

    }
}
