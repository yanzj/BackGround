using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    public class MrsSinrUlStat : IEntity<ObjectId>, IStatDateCell
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string CellId { get; set; }

        public DateTime StatDate { get; set; }

        public int SinrUL_00 { get; set; }

        public int SinrUL_01 { get; set; }

        public int SinrUL_02 { get; set; }

        public int SinrUL_03 { get; set; }

        public int SinrUL_04 { get; set; }

        public int SinrUL_05 { get; set; }

        public int SinrUL_06 { get; set; }

        public int SinrUL_07 { get; set; }

        public int SinrUL_08 { get; set; }

        public int SinrUL_09 { get; set; }

        public int SinrUL_10 { get; set; }

        public int SinrUL_11 { get; set; }

        public int SinrUL_12 { get; set; }

        public int SinrUL_13 { get; set; }

        public int SinrUL_14 { get; set; }

        public int SinrUL_15 { get; set; }

        public int SinrUL_16 { get; set; }

        public int SinrUL_17 { get; set; }

        public int SinrUL_18 { get; set; }

        public int SinrUL_19 { get; set; }

        public int SinrUL_20 { get; set; }

        public int SinrUL_21 { get; set; }

        public int SinrUL_22 { get; set; }

        public int SinrUL_23 { get; set; }

        public int SinrUL_24 { get; set; }

        public int SinrUL_25 { get; set; }

        public int SinrUL_26 { get; set; }

        public int SinrUL_27 { get; set; }

        public int SinrUL_28 { get; set; }

        public int SinrUL_29 { get; set; }

        public int SinrUL_30 { get; set; }

        public int SinrUL_31 { get; set; }

        public int SinrUL_32 { get; set; }

        public int SinrUL_33 { get; set; }

        public int SinrUL_34 { get; set; }

        public int SinrUL_35 { get; set; }

        public int SinrUL_36 { get; set; }
    }
}