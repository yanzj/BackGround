using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
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

    public class PDSCHCfg : IEntity<ObjectId>, IHuaweiCellMongo
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

        public int Pb { get; set; }

        public int ReferenceSignalPwrMargin { get; set; }

        public int ReferenceSignalPwr { get; set; }

        public double RsPower => ReferenceSignalPwr * 0.1;

        public int? TxPowerOffsetAnt0 { get; set; }

        public int? TxPowerOffsetAnt1 { get; set; }

        public int? TxPowerOffsetAnt2 { get; set; }

        public int? TxPowerOffsetAnt3 { get; set; }

        public int? TxChnPowerCfgSw { get; set; }
    }
}
