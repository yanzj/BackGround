using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Abp.EntityFramework.Entities.Channel
{
    public class CellDlpcPdschPa : IEntity<ObjectId>, IHuaweiCellMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int LocalCellId { get; set; }

        public int PdschPaAdjSwitch { get; set; }

        public int PaPcOff { get; set; }

        public int? NomPdschRsEpreOffset { get; set; }
    }
}
