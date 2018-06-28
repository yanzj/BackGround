using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;
using System;

namespace Lte.Parameters.Entities.Kpi
{
    public class MrsPhrStat : IEntity<ObjectId>, IStatDateCell
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string CellId { get; set; }

        public DateTime StatDate { get; set; }

        public int PowerHeadRoom_00 { get; set; }

        public int PowerHeadRoom_01 { get; set; }

        public int PowerHeadRoom_02 { get; set; }

        public int PowerHeadRoom_03 { get; set; }

        public int PowerHeadRoom_04 { get; set; }

        public int PowerHeadRoom_05 { get; set; }

        public int PowerHeadRoom_06 { get; set; }

        public int PowerHeadRoom_07 { get; set; }

        public int PowerHeadRoom_08 { get; set; }

        public int PowerHeadRoom_09 { get; set; }

        public int PowerHeadRoom_10 { get; set; }

        public int PowerHeadRoom_11 { get; set; }

        public int PowerHeadRoom_12 { get; set; }

        public int PowerHeadRoom_13 { get; set; }

        public int PowerHeadRoom_14 { get; set; }

        public int PowerHeadRoom_15 { get; set; }

        public int PowerHeadRoom_16 { get; set; }

        public int PowerHeadRoom_17 { get; set; }

        public int PowerHeadRoom_18 { get; set; }

        public int PowerHeadRoom_19 { get; set; }

        public int PowerHeadRoom_20 { get; set; }

        public int PowerHeadRoom_21 { get; set; }

        public int PowerHeadRoom_22 { get; set; }

        public int PowerHeadRoom_23 { get; set; }

        public int PowerHeadRoom_24 { get; set; }

        public int PowerHeadRoom_25 { get; set; }

        public int PowerHeadRoom_26 { get; set; }

        public int PowerHeadRoom_27 { get; set; }

        public int PowerHeadRoom_28 { get; set; }

        public int PowerHeadRoom_29 { get; set; }

        public int PowerHeadRoom_30 { get; set; }

        public int PowerHeadRoom_31 { get; set; }

        public int PowerHeadRoom_32 { get; set; }

        public int PowerHeadRoom_33 { get; set; }

        public int PowerHeadRoom_34 { get; set; }

        public int PowerHeadRoom_35 { get; set; }

        public int PowerHeadRoom_36 { get; set; }

        public int PowerHeadRoom_37 { get; set; }

        public int PowerHeadRoom_38 { get; set; }

        public int PowerHeadRoom_39 { get; set; }

        public int PowerHeadRoom_40 { get; set; }

        public int PowerHeadRoom_41 { get; set; }

        public int PowerHeadRoom_42 { get; set; }

        public int PowerHeadRoom_43 { get; set; }

        public int PowerHeadRoom_44 { get; set; }

        public int PowerHeadRoom_45 { get; set; }

        public int PowerHeadRoom_46 { get; set; }

        public int PowerHeadRoom_47 { get; set; }

        public int PowerHeadRoom_48 { get; set; }

        public int PowerHeadRoom_49 { get; set; }

        public int PowerHeadRoom_50 { get; set; }

        public int PowerHeadRoom_51 { get; set; }

        public int PowerHeadRoom_52 { get; set; }

        public int PowerHeadRoom_53 { get; set; }

        public int PowerHeadRoom_54 { get; set; }

        public int PowerHeadRoom_55 { get; set; }

        public int PowerHeadRoom_56 { get; set; }

        public int PowerHeadRoom_57 { get; set; }

        public int PowerHeadRoom_58 { get; set; }

        public int PowerHeadRoom_59 { get; set; }

        public int PowerHeadRoom_60 { get; set; }

        public int PowerHeadRoom_61 { get; set; }

        public int PowerHeadRoom_62 { get; set; }

        public int PowerHeadRoom_63 { get; set; }
    }
}
