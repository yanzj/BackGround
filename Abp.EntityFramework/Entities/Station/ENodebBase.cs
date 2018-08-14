using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Station
{
    [AutoMapFrom(typeof(ENodebBaseExcel))]
    [TypeDoc("��������ƽ̨��վ����")]
    public class ENodebBase : Entity
    {
        [MemberDoc("eNBID")]
        public int ENodebId { get; set; }

        [MemberDoc("����վַ����")]
        public string StationNum { get; set; }

        [MemberDoc("����վַ����")]
        public string StationName { get; set; }

        [MemberDoc("��������վַ����")]
        public string TowerStationNum { get; set; }

        [MemberDoc("��������վַ����")]
        public string TowerStationName { get; set; }

        [MemberDoc("��/��/��/��")]
        public string StationDistrict { get; set; }

        [MemberDoc("��/��/�ֵ�")]
        public string StationTown { get; set; }

        [MemberDoc("��С��Ԫ")]
        public string SmallUnit { get; set; }

        [MemberDoc("Ӫ������/Ӫҵ��")]
        public string MarketCenter { get; set; }

        [MemberDoc("Ƭ��")]
        public string StationRegion { get; set; }

        [MemberDoc("��")]
        public string StationCluster { get; set; }

        [MemberDoc("����")]
        public string Grid { get; set; }

        [MemberDoc("eNBID����")]
        public string ENodebName { get; set; }

        [MemberDoc("eNBID�ɼ�����")]
        public string ENodebFormalName { get; set; }

        [MemberDoc("����")]
        [AutoMapPropertyResolve("ENodebFactoryDescription", typeof(ENodebBaseExcel), typeof(ENodebFactoryTransform))]
        public ENodebFactory ENodebFactory { get; set; }

        [MemberDoc("�豸�ͺ�")]
        public string ApplianceModel { get; set; }

        [MemberDoc("IPV4��ַ")]
        public string Ipv4Address { get; set; }

        [MemberDoc("��������")]
        public string SubNetMask { get; set; }

        [MemberDoc("���ص�ַ")]
        public string GatewayIp { get; set; }

        [MemberDoc("S1���ô���(Mbps)")]
        public double? S1Bandwidth { get; set; }

        [MemberDoc("����MME-1��ʶ")]
        public string Mme1Info { get; set; }

        [MemberDoc("����MME-2��ʶ")]
        public string Mme2Info { get; set; }

        [MemberDoc("eNBID�����汾")]
        public string ENodebSoftwareVersion { get; set; }

        [MemberDoc("˫��ģʽ")]
        [AutoMapPropertyResolve("DuplexingDescription", typeof(ENodebBaseExcel), typeof(DuplexingTransform))]
        public Duplexing Duplexing { get; set; }

        [MemberDoc("С������")]
        public int? TotalCells { get; set; }

        [MemberDoc("omc�л�վ����״̬")]
        [AutoMapPropertyResolve("OmcStateDescription", typeof(ENodebBaseExcel), typeof(OmcStateTransform))]
        public OmcState OmcState { get; set; }

        [MemberDoc("��վ�������к�")]
        public string ENodebSerial { get; set; }

        [MemberDoc("��վ����")]
        [AutoMapPropertyResolve("ENodebTypeDescription", typeof(ENodebBaseExcel), typeof(ENodebTypeTransform))]
        public ENodebType ENodebType { get; set; }

        [MemberDoc("��վ�ȼ�")]
        [AutoMapPropertyResolve("ENodebClassDescription", typeof(ENodebBaseExcel), typeof(ENodebClassTransform))]
        public ENodebClass ENodebClass { get; set; }

        [MemberDoc("��վ����")]
        public double? Longtitute { get; set; }

        [MemberDoc("��վγ��")]
        public double? Lattitute { get; set; }

        [MemberDoc("���̱���")]
        public string ProjectSerial { get; set; }

        [MemberDoc("�Ƿ�����վ")]
        [AutoMapPropertyResolve("ENodebShared", typeof(ENodebBaseExcel), typeof(YesToBoolTransform))]
        public bool IsENodebShared { get; set; }

        [MemberDoc("OMCIP��ַ")]
        public string OmcIp { get; set; }

        [MemberDoc("��������")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("��������")]
        public DateTime? OpenTime { get; set; }

        [MemberDoc("Ƶ�α�ʶ")]
        public string BandClass { get; set; }

        [MemberDoc("ҵ������")]
        public string ServiceType { get; set; }

        [MemberDoc("��������ά����ʽ")]
        public string OpenTimeUpdateFunction { get; set; }

        [MemberDoc("��ע")]
        public string Comments { get; set; }

        [MemberDoc("�����˹�����ʱ��")]
        public DateTime? DataUpdateTime { get; set; }

        [MemberDoc("���ݸ�����")]
        public string DataUpdater { get; set; }

        [MemberDoc("�Զ���1")]
        public string Customize1 { get; set; }

        [MemberDoc("�Զ���2")]
        public string Customize2 { get; set; }

        [MemberDoc("�Զ���3")]
        public string Customize3 { get; set; }
    }
}