using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    public class MrsTadvStat : IEntity<ObjectId>, IStatDateCell, ILteCellReadOnly
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

        public int Tadv_00 { get; set; }

        public int Tadv_01 { get; set; }

        public int Tadv_02 { get; set; }

        public int Tadv_03 { get; set; }

        public int Tadv_04 { get; set; }

        public int Tadv_05 { get; set; }

        public int Tadv_06 { get; set; }

        public int Tadv_07 { get; set; }

        public int Tadv_08 { get; set; }

        public int Tadv_09 { get; set; }

        public int Tadv_10 { get; set; }

        public int Tadv_11 { get; set; }

        public int Tadv_12 { get; set; }

        public int Tadv_13 { get; set; }

        public int Tadv_14 { get; set; }

        public int Tadv_15 { get; set; }

        public int Tadv_16 { get; set; }

        public int Tadv_17 { get; set; }

        public int Tadv_18 { get; set; }

        public int Tadv_19 { get; set; }

        public int Tadv_20 { get; set; }

        public int Tadv_21 { get; set; }

        public int Tadv_22 { get; set; }

        public int Tadv_23 { get; set; }

        public int Tadv_24 { get; set; }

        public int Tadv_25 { get; set; }

        public int Tadv_26 { get; set; }

        public int Tadv_27 { get; set; }

        public int Tadv_28 { get; set; }

        public int Tadv_29 { get; set; }

        public int Tadv_30 { get; set; }

        public int Tadv_31 { get; set; }

        public int Tadv_32 { get; set; }

        public int Tadv_33 { get; set; }

        public int Tadv_34 { get; set; }

        public int Tadv_35 { get; set; }

        public int Tadv_36 { get; set; }

        public int Tadv_37 { get; set; }

        public int Tadv_38 { get; set; }

        public int Tadv_39 { get; set; }

        public int Tadv_40 { get; set; }

        public int Tadv_41 { get; set; }

        public int Tadv_42 { get; set; }

        public int Tadv_43 { get; set; }

        public int Tadv_44 { get; set; }

    }
}