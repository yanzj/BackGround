using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapTo(typeof(PreciseCoverage4G))]
    public class PreciseMongo : IEntity<ObjectId>, IStatDateCell
    {
        public bool IsTransient()
        {
            return false;
        }

        [IgnoreMap]
        public ObjectId Id { get; set; }
        
        [AutoMapPropertyResolve("Id", typeof(PreciseCoverage4G))]
        public int FakeId => 0;

        [IgnoreMap]
        public string CellId { get; set; }

        [AutoMapPropertyResolve("CellId", typeof(PreciseCoverage4G))]
        public int ENodebId => CellId.GetSplittedFields('-')[0].ConvertToInt(0);

        public byte SectorId => CellId.GetSplittedFields('-')[1].ConvertToByte(0);

        public int Pci { get; set; }

        [AutoMapPropertyResolve("StatTime", typeof(PreciseCoverage4G))]
        public DateTime StatDate { get; set; }

        public int Neighbors0 { get; set; }

        public int Neighbors1 { get; set; }

        public int Neighbors2 { get; set; }

        public int Neighbors3 { get; set; }

        public int NeighborsMore { get; set; }

        public int IntraNeighbors0 { get; set; }

        public int IntraNeighbors1 { get; set; }

        public int IntraNeighbors2 { get; set; }

        public int IntraNeighbors3 { get; set; }

        public int IntraNeighborsMore { get; set; }

        public int TotalMrs
            => IntraNeighbors0 + IntraNeighbors1 + IntraNeighbors2 + IntraNeighbors3 + IntraNeighborsMore;

        public int FirstNeighbors => IntraNeighbors1 + IntraNeighbors2 + IntraNeighbors3 + IntraNeighborsMore;

        public int SecondNeighbors => IntraNeighbors2 + IntraNeighbors3 + IntraNeighborsMore;

        public int ThirdNeighbors => IntraNeighbors3 + IntraNeighborsMore;
    }
}