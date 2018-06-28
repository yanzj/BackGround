using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    public class MrsTadvRsrpStat : IEntity<ObjectId>, IStatDateCell
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string CellId { get; set; }

        public DateTime StatDate { get; set; }

        public int Tadv00Rsrp00 { get; set; }

        public int Tadv00Rsrp01 { get; set; }

        public int Tadv00Rsrp02 { get; set; }

        public int Tadv00Rsrp03 { get; set; }

        public int Tadv00Rsrp04 { get; set; }

        public int Tadv00Rsrp05 { get; set; }

        public int Tadv00Rsrp06 { get; set; }

        public int Tadv00Rsrp07 { get; set; }

        public int Tadv00Rsrp08 { get; set; }

        public int Tadv00Rsrp09 { get; set; }

        public int Tadv00Rsrp10 { get; set; }

        public int Tadv00Rsrp11 { get; set; }

        public int Tadv01Rsrp00 { get; set; }

        public int Tadv01Rsrp01 { get; set; }

        public int Tadv01Rsrp02 { get; set; }

        public int Tadv01Rsrp03 { get; set; }

        public int Tadv01Rsrp04 { get; set; }

        public int Tadv01Rsrp05 { get; set; }

        public int Tadv01Rsrp06 { get; set; }

        public int Tadv01Rsrp07 { get; set; }

        public int Tadv01Rsrp08 { get; set; }

        public int Tadv01Rsrp09 { get; set; }

        public int Tadv01Rsrp10 { get; set; }

        public int Tadv01Rsrp11 { get; set; }

        public int Tadv02Rsrp00 { get; set; }

        public int Tadv02Rsrp01 { get; set; }

        public int Tadv02Rsrp02 { get; set; }

        public int Tadv02Rsrp03 { get; set; }

        public int Tadv02Rsrp04 { get; set; }

        public int Tadv02Rsrp05 { get; set; }

        public int Tadv02Rsrp06 { get; set; }

        public int Tadv02Rsrp07 { get; set; }

        public int Tadv02Rsrp08 { get; set; }

        public int Tadv02Rsrp09 { get; set; }

        public int Tadv02Rsrp10 { get; set; }

        public int Tadv02Rsrp11 { get; set; }

        public int Tadv03Rsrp00 { get; set; }

        public int Tadv03Rsrp01 { get; set; }

        public int Tadv03Rsrp02 { get; set; }

        public int Tadv03Rsrp03 { get; set; }

        public int Tadv03Rsrp04 { get; set; }

        public int Tadv03Rsrp05 { get; set; }

        public int Tadv03Rsrp06 { get; set; }

        public int Tadv03Rsrp07 { get; set; }

        public int Tadv03Rsrp08 { get; set; }

        public int Tadv03Rsrp09 { get; set; }

        public int Tadv03Rsrp10 { get; set; }

        public int Tadv03Rsrp11 { get; set; }

        public int Tadv04Rsrp00 { get; set; }

        public int Tadv04Rsrp01 { get; set; }

        public int Tadv04Rsrp02 { get; set; }

        public int Tadv04Rsrp03 { get; set; }

        public int Tadv04Rsrp04 { get; set; }

        public int Tadv04Rsrp05 { get; set; }

        public int Tadv04Rsrp06 { get; set; }

        public int Tadv04Rsrp07 { get; set; }

        public int Tadv04Rsrp08 { get; set; }

        public int Tadv04Rsrp09 { get; set; }

        public int Tadv04Rsrp10 { get; set; }

        public int Tadv04Rsrp11 { get; set; }

        public int Tadv05Rsrp00 { get; set; }

        public int Tadv05Rsrp01 { get; set; }

        public int Tadv05Rsrp02 { get; set; }

        public int Tadv05Rsrp03 { get; set; }

        public int Tadv05Rsrp04 { get; set; }

        public int Tadv05Rsrp05 { get; set; }

        public int Tadv05Rsrp06 { get; set; }

        public int Tadv05Rsrp07 { get; set; }

        public int Tadv05Rsrp08 { get; set; }

        public int Tadv05Rsrp09 { get; set; }

        public int Tadv05Rsrp10 { get; set; }

        public int Tadv05Rsrp11 { get; set; }

        public int Tadv06Rsrp00 { get; set; }

        public int Tadv06Rsrp01 { get; set; }

        public int Tadv06Rsrp02 { get; set; }

        public int Tadv06Rsrp03 { get; set; }

        public int Tadv06Rsrp04 { get; set; }

        public int Tadv06Rsrp05 { get; set; }

        public int Tadv06Rsrp06 { get; set; }

        public int Tadv06Rsrp07 { get; set; }

        public int Tadv06Rsrp08 { get; set; }

        public int Tadv06Rsrp09 { get; set; }

        public int Tadv06Rsrp10 { get; set; }

        public int Tadv06Rsrp11 { get; set; }

        public int Tadv07Rsrp00 { get; set; }

        public int Tadv07Rsrp01 { get; set; }

        public int Tadv07Rsrp02 { get; set; }

        public int Tadv07Rsrp03 { get; set; }

        public int Tadv07Rsrp04 { get; set; }

        public int Tadv07Rsrp05 { get; set; }

        public int Tadv07Rsrp06 { get; set; }

        public int Tadv07Rsrp07 { get; set; }

        public int Tadv07Rsrp08 { get; set; }

        public int Tadv07Rsrp09 { get; set; }

        public int Tadv07Rsrp10 { get; set; }

        public int Tadv07Rsrp11 { get; set; }

        public int Tadv08Rsrp00 { get; set; }

        public int Tadv08Rsrp01 { get; set; }

        public int Tadv08Rsrp02 { get; set; }

        public int Tadv08Rsrp03 { get; set; }

        public int Tadv08Rsrp04 { get; set; }

        public int Tadv08Rsrp05 { get; set; }

        public int Tadv08Rsrp06 { get; set; }

        public int Tadv08Rsrp07 { get; set; }

        public int Tadv08Rsrp08 { get; set; }

        public int Tadv08Rsrp09 { get; set; }

        public int Tadv08Rsrp10 { get; set; }

        public int Tadv08Rsrp11 { get; set; }

        public int Tadv09Rsrp00 { get; set; }

        public int Tadv09Rsrp01 { get; set; }

        public int Tadv09Rsrp02 { get; set; }

        public int Tadv09Rsrp03 { get; set; }

        public int Tadv09Rsrp04 { get; set; }

        public int Tadv09Rsrp05 { get; set; }

        public int Tadv09Rsrp06 { get; set; }

        public int Tadv09Rsrp07 { get; set; }

        public int Tadv09Rsrp08 { get; set; }

        public int Tadv09Rsrp09 { get; set; }

        public int Tadv09Rsrp10 { get; set; }

        public int Tadv09Rsrp11 { get; set; }

        public int Tadv10Rsrp00 { get; set; }

        public int Tadv10Rsrp01 { get; set; }

        public int Tadv10Rsrp02 { get; set; }

        public int Tadv10Rsrp03 { get; set; }

        public int Tadv10Rsrp04 { get; set; }

        public int Tadv10Rsrp05 { get; set; }

        public int Tadv10Rsrp06 { get; set; }

        public int Tadv10Rsrp07 { get; set; }

        public int Tadv10Rsrp08 { get; set; }

        public int Tadv10Rsrp09 { get; set; }

        public int Tadv10Rsrp10 { get; set; }

        public int Tadv10Rsrp11 { get; set; }

    }
}