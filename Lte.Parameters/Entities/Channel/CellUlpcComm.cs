using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Regular.Attributes;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
{
    [TypeDoc("华为上行控制参数")]
    public class CellUlpcComm : IEntity<ObjectId>, IHuaweiCellMongo
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        [MemberDoc("消息2功率偏置, 该参数为上行随机接入过程中的Msg3的功率偏置值，该参数也用于控制PUSCH/PUCCH的闭环功控累积量初始值。该参数仅适用于TDD。")]
        public int DeltaMsg2 { get; set; }

        [MemberDoc("PUCCH格式2b的偏置，该参数表示PUCCH格式2b的Delta值。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaFPUCCHFormat2b { get; set; }

        [MemberDoc("PUSCH标称P0值：该参数表示PUSCH的标称P0值，应用于上行功控过程，参数的使用细节请参见3GPP TS 36.213。")]
        public int P0NominalPUSCH { get; set; }

        [MemberDoc("PUCCH格式2a的偏置：该参数表示PUCCH格式2a的Delta值。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaFPUCCHFormat2a { get; set; }

        [MemberDoc("消息3相对前导的功率偏置：该参数表示消息3的前导Delta值，步长为2。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaPreambleMsg3 { get; set; }

        [MemberDoc("PUCCH格式1b的偏置：该参数表示PUCCH格式1b的Delta值。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaFPUCCHFormat1b { get; set; }

        [MemberDoc("PUCCH标称P0值：该参数表示正常进行PUCCH解调，eNodeB所期望的PUCCH发射功率水平。参数的使用细节请参见3GPP TS 36.213。")]
        public int P0NominalPUCCH { get; set; }

        [MemberDoc("PUCCH格式1的偏置：该参数表示PUCCH格式1的Delta值。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaFPUCCHFormat1 { get; set; }

        [MemberDoc("路径损耗因子：该参数表示路径损耗补偿因子，应用于上行功控过程，参数的使用细节请参见3GPP TS 36.213。")]
        public int PassLossCoeff { get; set; }

        [MemberDoc("PUCCH格式2的偏置：该参数表示PUCCH格式2的Delta值。参数的使用细节请参见3GPP TS 36.213。")]
        public int DeltaFPUCCHFormat2 { get; set; }

        public int LocalCellId { get; set; }

        public int? DeltaFPUCCHFormat1bc { get; set; }

        public int? DeltaFPUCCHFormat1bcs { get; set; }

        public int? DeltaFPUCCHFormat3 { get; set; }
    }
}