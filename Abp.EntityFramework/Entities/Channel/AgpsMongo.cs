using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using MongoDB.Bson;

namespace Abp.EntityFramework.Entities.Channel
{
    public class AgpsMongo : IEntity<ObjectId>, IStatDateCell, IGeoPointReadonly<double>
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public DateTime StatDate { get; set; }

        public string CellId { get; set; }

        public int ENodebId => CellId.GetSplittedFields('-')[0].ConvertToInt(0);

        [ArraySumProtection]
        public int X { get; set; }

        [ArraySumProtection]
        public int Y { get; set; }

        public double Longtitute => 112 + X*0.00049;

        public double Lattitute => 22 + Y*0.00045;

        public int Rsrp { get; set; }

        public int Count { get; set; }

        public int GoodCount { get; set; }

        public int GoodCount105 { get; set; }

        public int GoodCount100 { get; set; }
    }
}
