using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
{
    public class ECellEquipmentFunctionZte : IEntity<ObjectId>, IZteMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public string supercellFlag { get; set; }

        public int antMapUl { get; set; }

        public int antMapDl { get; set; }

        public string refRfDevice { get; set; }

        public int insPortCmpModDl { get; set; }

        public string ref1SdrDeviceGroup { get; set; }

        public string anttoPortMap { get; set; }

        public int ECellEquipmentFunction { get; set; }

        public int aasTiltUl { get; set; }

        public int bplType { get; set; }

        public int cpTransTime { get; set; }

        public double cpTransPwr { get; set; }

        public string reservedByEUtranCellFDD { get; set; }

        public int dlTransInd { get; set; }

        public double cpSpeRefSigPwr { get; set; }

        public string refSdrDeviceGroup { get; set; }

        public int rfAppMode { get; set; }

        public double maxCPTransPwr { get; set; }

        public int bplPort { get; set; }

        public int adminState { get; set; }

        public int aasTiltDl { get; set; }

        public int upActAntBitmap { get; set; }

        public int rruCarrierNo { get; set; }

        public int dynBaseBandPoolSwch { get; set; }

        public string refBpDevice { get; set; }

        public int cellMod { get; set; }

        public int slaveRRUFlag { get; set; }

        public int cpId { get; set; }
    }
}