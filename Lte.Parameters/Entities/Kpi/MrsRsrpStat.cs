using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    public class MrsRsrpStat : IEntity<ObjectId>, IStatDateCell
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string CellId { get; set; }

        public int ENodebId => CellId.GetSplittedFields('-')[0].ConvertToInt(0);

        public byte SectorId => CellId.GetSplittedFields('-')[1].ConvertToByte(0);

        public DateTime StatDate { get; set; }

        public int RSRP_00 { get; set; }

        public int RSRP_01 { get; set; }

        public int RSRP_02 { get; set; }

        public int RSRP_03 { get; set; }

        public int RSRP_04 { get; set; }

        public int RSRP_05 { get; set; }

        public int RSRP_06 { get; set; }

        public int RSRP_07 { get; set; }

        public int RSRP_08 { get; set; }

        public int RSRP_09 { get; set; }

        public int RSRP_10 { get; set; }

        public int RSRP_11 { get; set; }

        public int RSRP_12 { get; set; }

        public int RSRP_13 { get; set; }

        public int RSRP_14 { get; set; }

        public int RSRP_15 { get; set; }

        public int RSRP_16 { get; set; }

        public int RSRP_17 { get; set; }

        public int RSRP_18 { get; set; }

        public int RSRP_19 { get; set; }

        public int RSRP_20 { get; set; }

        public int RSRP_21 { get; set; }

        public int RSRP_22 { get; set; }

        public int RSRP_23 { get; set; }

        public int RSRP_24 { get; set; }

        public int RSRP_25 { get; set; }

        public int RSRP_26 { get; set; }

        public int RSRP_27 { get; set; }

        public int RSRP_28 { get; set; }

        public int RSRP_29 { get; set; }

        public int RSRP_30 { get; set; }

        public int RSRP_31 { get; set; }

        public int RSRP_32 { get; set; }

        public int RSRP_33 { get; set; }

        public int RSRP_34 { get; set; }

        public int RSRP_35 { get; set; }

        public int RSRP_36 { get; set; }

        public int RSRP_37 { get; set; }

        public int RSRP_38 { get; set; }

        public int RSRP_39 { get; set; }

        public int RSRP_40 { get; set; }

        public int RSRP_41 { get; set; }

        public int RSRP_42 { get; set; }

        public int RSRP_43 { get; set; }

        public int RSRP_44 { get; set; }

        public int RSRP_45 { get; set; }

        public int RSRP_46 { get; set; }

        public int RSRP_47 { get; set; }
    }
}