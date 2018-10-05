using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using AutoMapper;
using Lte.Domain.Common.Types;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapTo(typeof(InterferenceMatrixStat))]
    public class InterferenceMatrixMongo : IEntity<ObjectId>, IStatDateCell
    {
        [AutoMapPropertyResolve("Id", typeof(InterferenceMatrixStat), typeof(IgnoreMapAttribute))]
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string CellId { get; set; }

        public int Pci { get; set; }

        public int NeighborPci { get; set; }

        public DateTime StatDate { get; set; }

        public int Diff0 { get; set; }

        public int Diff3 { get; set; }

        public int Diff6 { get; set; }

        public int Diff9 { get; set; }

        public int Diff12 { get; set; }

        public int DiffLarge { get; set; }

        public int SinrUl0to9 { get; set; }

        public int SinrUl10to19 { get; set; }

        public int SinrUl20to24 { get; set; }

        public int SinrUl25to29 { get; set; }

        public int SinrUl30to34 { get; set; }

        public int SinrUlAbove35 { get; set; }

        public int RsrpBelow120 { get; set; }

        public int RsrpBetween120110 { get; set; }

        public int RsrpBetween110105 { get; set; }

        public int RsrpBetween105100 { get; set; }

        public int RsrpBetween10090 { get; set; }

        public int? RsrpAbove90 { get; set; }

        public int? NeighborRsrpBelow120 { get; set; }

        public int? NeighborRsrpBetween120110 { get; set; }

        public int? NeighborRsrpBetween110105 { get; set; }

        public int? NeighborRsrpBetween105100 { get; set; }

        public int? NeighborRsrpBetween10090 { get; set; }

        public int? NeighborRsrpAbove90 { get; set; }

        public int Ta0or1 { get; set; }

        public int Ta2or3 { get; set; }

        public int Ta4or5 { get; set; }

        public int Ta6or7 { get; set; }

        public int Ta8or9 { get; set; }

        public int Ta10to12 { get; set; }

        public int Ta13to15 { get; set; }

        public int Ta16to19 { get; set; }

        public int Ta20to24 { get; set; }

        public int Ta25to29 { get; set; }

        public int Ta30to39 { get; set; }

        public int TaAbove40 { get; set; }

        public int? Earfcn { get; set; }

        public int? NeighborEarfcn { get; set; }
    }
}